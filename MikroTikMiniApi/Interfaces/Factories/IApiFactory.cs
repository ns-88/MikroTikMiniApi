using System.Net;
using MikroTikMiniApi.Interfaces.Networking;

namespace MikroTikMiniApi.Interfaces.Factories
{
    public interface IApiFactory
    {
        IControlledConnection CreateConnection(IPEndPoint endPoint);
        IRouterApi CreateRouterApi(IConnection connection);
    }
}