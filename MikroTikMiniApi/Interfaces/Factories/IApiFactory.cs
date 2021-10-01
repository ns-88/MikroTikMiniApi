﻿using System.Net;
using MikroTikMiniApi.Interfaces.Models.Settings;
using MikroTikMiniApi.Interfaces.Networking;

namespace MikroTikMiniApi.Interfaces.Factories
{
    public interface IApiFactory
    {
        IApiSentenceFactory ApiSentenceFactory { get; }

        IControlledConnection CreateConnection(IPEndPoint endPoint);

        IControlledConnection CreateConnection(IConnectionSettings settings);

        IRouterApi CreateRouterApi(IConnection connection);
    }
}