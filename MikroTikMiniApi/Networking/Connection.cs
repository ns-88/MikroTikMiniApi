using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MikroTikMiniApi.Interfaces.Models.Settings;
using MikroTikMiniApi.Interfaces.Networking;
using MikroTikMiniApi.Models.Settings;
using MikroTikMiniApi.Utilities;

namespace MikroTikMiniApi.Networking
{
    internal class Connection : IControlledConnection
    {
        private readonly IConnectionSettings _settings;
        private readonly Socket _socket;
        private readonly SemaphoreSlim _semaphoreSendLock;
        private readonly SemaphoreSlim _semaphoreReceiveLock;
        private CancellationTokenSource _ctsSend;
        private CancellationTokenSource _ctsReceive;

        private Connection()
        {
            _semaphoreSendLock = new SemaphoreSlim(1);
            _semaphoreReceiveLock = new SemaphoreSlim(1);
            _ctsSend = new CancellationTokenSource();
            _ctsReceive = new CancellationTokenSource();
        }

        public Connection(IPEndPoint endPoint) : this()
        {
            Guard.ThrowIfNull(endPoint, nameof(endPoint));

            _settings = new ConnectionSettings(endPoint);
            _socket = CreateSocket(_settings);
        }

        public Connection(IConnectionSettings settings) : this()
        {
            Guard.ThrowIfNull(settings, out _settings, nameof(settings));
            _socket = CreateSocket(settings);
        }

        private static Socket CreateSocket(IConnectionSettings settings)
        {
            return new Socket(settings.EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public async ValueTask ConnectAsync()
        {
            using var cts = new CancellationTokenSource(_settings.ConnectionTimeout);

            try
            {
                await _socket.ConnectAsync(_settings.EndPoint, cts.Token).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw new TimeoutException("Подключение не было выполнено за установленный период времени.");
            }
        }

        public async ValueTask ReceiveAsync(Memory<byte> buffer)
        {
            var length = buffer.Length;
            var totalReceived = 0;

            await _semaphoreReceiveLock.WaitAsync().ConfigureAwait(false);

            try
            {
                _ctsReceive.CancelAfter(_settings.ReceiveTimeout);

                try
                {
                    while (true)
                    {
                        var received = await _socket.ReceiveAsync(buffer, SocketFlags.None, _ctsReceive.Token).ConfigureAwait(false);

                        if (received == 0)
                            throw new InvalidOperationException("Потеряна связь с удаленным хостом.");

                        totalReceived += received;

                        if (totalReceived == length)
                            break;

                        buffer = buffer.Slice(received);
                    }
                }
                finally
                {
                    _ctsReceive.CancelAfter(Timeout.InfiniteTimeSpan);
                }
            }
            catch (OperationCanceledException)
            {
                _ctsReceive = new CancellationTokenSource();
                throw new TimeoutException("Получение данных не было выполнено за установленный период времени.");
            }
            finally
            {
                _semaphoreReceiveLock.Release();
            }
        }

        public async ValueTask SendAsync(ReadOnlyMemory<byte> buffer)
        {
            var length = buffer.Length;
            var totalSent = 0;

            await _semaphoreSendLock.WaitAsync().ConfigureAwait(false);

            try
            {
                _ctsSend.CancelAfter(_settings.SendTimeout);

                try
                {
                    while (true)
                    {
                        var sent = await _socket.SendAsync(buffer, SocketFlags.None).ConfigureAwait(false);

                        totalSent += sent;

                        if (totalSent == length)
                            break;

                        buffer = buffer.Slice(sent);
                    }
                }
                finally
                {
                    _ctsSend.CancelAfter(Timeout.InfiniteTimeSpan);
                }
            }
            catch (OperationCanceledException)
            {
                _ctsSend = new CancellationTokenSource();
                throw new TimeoutException("Отправка данных не была выполнена за установленный период времени.");
            }
            finally
            {
                _semaphoreSendLock.Release();
            }
        }

        public void Dispose()
        {
            _socket?.Dispose();
            _semaphoreSendLock?.Dispose();
            _semaphoreReceiveLock?.Dispose();
            _ctsSend?.Dispose();
            _ctsReceive?.Dispose();
        }
    }
}