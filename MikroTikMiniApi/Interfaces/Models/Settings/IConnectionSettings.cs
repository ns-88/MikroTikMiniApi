using System;
using System.Net;

namespace MikroTikMiniApi.Interfaces.Models.Settings
{
    /// <summary>
    /// Connection settings.
    /// </summary>
    public interface IConnectionSettings
    {
        /// <summary>
        /// End point. Default value: address = 192.168.88.1, port = 8728.
        /// </summary>
        IPEndPoint EndPoint { get; }

        /// <summary>
        /// Connection timeout. Default value: 20 sec.
        /// </summary>
        TimeSpan ConnectionTimeout { get; }

        /// <summary>
        /// Sending timeout. Default value: 30 sec.
        /// </summary>
        TimeSpan SendTimeout { get; }

        /// <summary>
        /// Receiving timeout. Default value: 30 sec.
        /// </summary>
        TimeSpan ReceiveTimeout { get; }
    }
}