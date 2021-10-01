using System.Net;
using System.Threading.Tasks;
using MikroTikMiniApi.Commands;
using MikroTikMiniApi.Factories;
using MikroTikMiniApi.Models.Api;

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
            
            await routerApi.AuthenticationAsync("name", "password");

            var sentence = await routerApi.ExecuteCommandAsync(ApiCommand.New("/interface/set")
                                                                         .AddParameter("disabled", "true")
                                                                         .AddParameter(".id", "ether1")
                                                                         .Build());

            var logs = await routerApi.ExecuteCommandToListAsync(ApiCommand.New("/log/print").Build());

            var packages = await routerApi.ExecuteCommandToListAsync<Package>(ApiCommand.New("/system/package/print").Build());

            await routerApi.QuitAsync();
        }
    }
}