using System.Net;
using MikroTikMiniApi.Interfaces;
using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Models.Settings;
using MikroTikMiniApi.Interfaces.Networking;
using MikroTikMiniApi.Networking;

namespace MikroTikMiniApi.Factories
{
    public class MicrotikApiFactory : IApiFactory
    {
        public IControlledConnection CreateConnection(IPEndPoint endPoint)
        {
            return new Connection(endPoint);
        }

        public IControlledConnection CreateConnection(IConnectionSettings settings)
        {
            return new Connection(settings);
        }

        public IRouterApi CreateRouterApi(IConnection connection)
        {
            return new MicrotikApi(connection);
        }
    }
}