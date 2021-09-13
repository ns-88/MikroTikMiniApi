using System;
using System.Threading.Tasks;
using MikroTikMiniApi.Interfaces.Networking;

namespace MikroTikMiniApi.Tests.Infrastructure.Networking
{
    internal abstract class FakeConnectionBase : IConnection
    {
        public abstract ValueTask ReceiveAsync(Memory<byte> buffer);

        public abstract ValueTask SendAsync(ReadOnlyMemory<byte> buffer);

        public static FakeConnectionSendCommand CreateConnectionSendCommand()
        {
            return new FakeConnectionSendCommand();
        }

        public static FakeConnectionReceiveCommand CreateConnectionReceiveCommand()
        {
            return new FakeConnectionReceiveCommand();
        }
    }
}