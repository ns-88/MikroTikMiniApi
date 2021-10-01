using System;
using System.Threading.Tasks;

namespace MikroTikMiniApi.Interfaces.Networking
{
    public interface IConnection
    {
        ValueTask ReceiveAsync(Memory<byte> buffer);
        ValueTask SendAsync(ReadOnlyMemory<byte> buffer);
    }

    public interface IControlledConnection : IConnection, IDisposable
    {
        ValueTask ConnectAsync();
    }
}