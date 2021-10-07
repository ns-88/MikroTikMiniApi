using System;
using System.Threading.Tasks;

namespace MikroTikMiniApi.Interfaces.Networking
{
    /// <summary>
    /// TCP connection.
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Receiving data.
        /// </summary>
        /// <param name="buffer">Buffer to receive.</param>
        /// <returns>A task to wait for.</returns>
        ValueTask ReceiveAsync(Memory<byte> buffer);
        /// <summary>
        /// Sending data.
        /// </summary>
        /// <param name="buffer">Buffer to send.</param>
        /// <returns>A task to wait for.</returns>
        ValueTask SendAsync(ReadOnlyMemory<byte> buffer);
    }

    /// <summary>
    /// TCP connection that allows you to establish a connection and clear resources.
    /// </summary>
    public interface IControlledConnection : IConnection, IDisposable
    {
        /// <summary>
        /// Connecting to a remote host.
        /// </summary>
        /// <returns>A task to wait for.</returns>
        ValueTask ConnectAsync();
    }
}