using System.Threading.Tasks;
using MikroTikMiniApi.Interfaces.Commands;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Interfaces.Services
{
    internal interface ICommandExecutionService
    {
        Task<IApiSentence> ExecuteCommandAsync(IApiCommand command);
    }
}