using System.Collections.Generic;
using System.Threading.Tasks;
using MikroTikMiniApi.Interfaces.Commands;
using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Models.Settings;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Interfaces.Services
{
    internal interface ICommandExecutionService
    {
        internal ValueTask SendCommandAsync(IApiCommand command);

        internal ValueTask<IApiSentence> ReceiveSentenceAsync();

        internal ValueTask<IReadOnlyList<IApiSentence>> FlushResponseStreamAsync(IExecutionSettings settings);

        Task<IApiSentence> ExecuteCommandAsync(IApiCommand command, IExecutionSettings settings = null);

        IAsyncEnumerable<IApiSentence> ExecuteCommandToEnumerableAsync(IApiCommand command, IExecutionSettings settings = null);

        Task<IReadOnlyList<IApiSentence>> ExecuteCommandToListAsync(IApiCommand command, IExecutionSettings settings = null);

        IAsyncEnumerable<T> ExecuteCommandToEnumerableAsync<T>(IApiCommand command, IExecutionSettings settings = null)
            where T : class, IModelFactory<T>, new();

        Task<IReadOnlyList<T>> ExecuteCommandToListAsync<T>(IApiCommand command, IExecutionSettings settings = null)
            where T : class, IModelFactory<T>, new();
    }
}