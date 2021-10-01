using System;
using System.Net;

namespace MikroTikMiniApi.Interfaces.Models.Settings
{
    public interface IConnectionSettings
    {
        IPEndPoint EndPoint { get; }
        TimeSpan ConnectionTimeout { get; }
        TimeSpan SendTimeout { get; }
        TimeSpan ReceiveTimeout { get; }
    }
}