using System.Net;
using System.Threading.Tasks;
using MikroTikMiniApi.Factories;

namespace MikroTikMiniApi.Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var apiFactory = new MicrotikApiFactory();
            using var connection = apiFactory.CreateConnection(new IPEndPoint(IPAddress.Parse("192.168.88.1"), 8728));

            await connection.ConnectAsync();

            var routerApi = apiFactory.CreateRouterApi(connection);

            await routerApi.AuthenticationAsync("user", "password");

            await routerApi.QuitAsync();
        }
    }
}