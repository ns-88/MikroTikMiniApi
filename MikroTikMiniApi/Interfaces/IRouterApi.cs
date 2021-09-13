using System.Threading.Tasks;
using MikroTikMiniApi.Interfaces.Commands;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Interfaces
{
    public interface IRouterApi
    {
        Task AuthenticationAsync(string name, string password);

        Task QuitAsync();

        Task<IApiSentence> ExecuteCommandAsync(IApiCommand command);
    }
}