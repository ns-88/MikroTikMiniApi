using System;
using System.Net;
using MikroTikMiniApi.Interfaces.Models.Settings;
using MikroTikMiniApi.Utilities;

namespace MikroTikMiniApi.Models.Settings
{
    ///<inheritdoc cref="IConnectionSettings"/>
    public class ConnectionSettings : IConnectionSettings
    {
        public static readonly ConnectionSettings Default;

        ///<inheritdoc/>
        public IPEndPoint EndPoint { get; private set; }

        ///<inheritdoc/>
        public TimeSpan ConnectionTimeout { get; private set; }

        ///<inheritdoc/>
        public TimeSpan SendTimeout { get; private set; }

        ///<inheritdoc/>
        public TimeSpan ReceiveTimeout { get; private set; }

        static ConnectionSettings()
        {
            Default = new ConnectionSettings(new IPEndPoint(IPAddress.Parse("192.168.88.1"), 8728));
        }

        public ConnectionSettings(IPEndPoint endPoint)
        {
            EndPoint = Guard.ThrowIfNullRet(endPoint, nameof(endPoint));
            ConnectionTimeout = TimeSpan.FromSeconds(20);
            SendTimeout = TimeSpan.FromSeconds(30);
            ReceiveTimeout = TimeSpan.FromSeconds(30);
        }

        #region Builder

        public ConnectionSettingsBuilder Builder => new(this);

        public class ConnectionSettingsBuilder
        {
            private readonly ConnectionSettings _settings;

            public ConnectionSettingsBuilder(IConnectionSettings settings)
            {
                _settings = new ConnectionSettings(settings.EndPoint)
                {
                    ConnectionTimeout = settings.ConnectionTimeout,
                    SendTimeout = settings.SendTimeout,
                    ReceiveTimeout = settings.ReceiveTimeout
                };
            }

            public ConnectionSettingsBuilder WithEndPoint(IPEndPoint endPoint)
            {
                _settings.EndPoint = Guard.ThrowIfNullRet(endPoint, nameof(endPoint));
                return this;
            }

            public ConnectionSettingsBuilder WithConnectionTimeout(TimeSpan timeout)
            {
                _settings.ConnectionTimeout = timeout;
                return this;
            }

            public ConnectionSettingsBuilder WithSendTimeout(TimeSpan timeout)
            {
                _settings.SendTimeout = timeout;
                return this;
            }

            public ConnectionSettingsBuilder WithReceiveTimeout(TimeSpan timeout)
            {
                _settings.ReceiveTimeout = timeout;
                return this;
            }

            public IConnectionSettings Build()
            {
                return _settings;
            }
        }

        #endregion
    }
}