using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MikroTikMiniApi.Interfaces.Networking;

namespace MikroTikMiniApi.Networking
{
    internal class Connection : IControlledConnection
    {
        private readonly IPEndPoint _ipEndPoint;
        private readonly Socket _socket;

        public Connection(IPEndPoint endPoint)
        {
            _ipEndPoint = endPoint ?? throw new ArgumentNullException(nameof(endPoint));
            _socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public Task ConnectAsync()
        {
            return _socket.ConnectAsync(_ipEndPoint);
        }

        public async ValueTask ReceiveAsync(Memory<byte> buffer)
        {
            var length = buffer.Length;
            var totalReceived = 0;

            while (true)
            {
                var received = await _socket.ReceiveAsync(buffer, SocketFlags.None).ConfigureAwait(false);

                if (received == 0)
                {
                    throw new InvalidOperationException("Потеряна связь с удаленным хостом.");
                }

                totalReceived += received;

                if (totalReceived == length)
                {
                    break;
                }

                buffer = buffer.Slice(received);
            }
        }

        public async ValueTask SendAsync(ReadOnlyMemory<byte> buffer)
        {
            var length = buffer.Length;
            var totalSent = 0;

            while (true)
            {
                var sent = await _socket.SendAsync(buffer, SocketFlags.None).ConfigureAwait(false);

                totalSent += sent;

                if (totalSent == length)
                {
                    break;
                }

                buffer = buffer.Slice(sent);
            }
        }

        public void Dispose()
        {
            _socket?.Dispose();
        }
    }
}