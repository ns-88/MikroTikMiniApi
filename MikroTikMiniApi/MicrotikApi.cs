using System.Collections.Generic;
using System.Threading.Tasks;
using MikroTikMiniApi.Interfaces;
using MikroTikMiniApi.Interfaces.Commands;
using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Networking;
using MikroTikMiniApi.Interfaces.Sentences;
using MikroTikMiniApi.Interfaces.Services;
using MikroTikMiniApi.Services;
using MikroTikMiniApi.Utilities;

namespace MikroTikMiniApi
{
    internal class MicrotikApi : IRouterApi
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICommandExecutionService _commandExecutionService;

        public MicrotikApi(IConnection connection)
        {
            Guard.ThrowIfNull(connection, nameof(connection));

            _commandExecutionService = new CommandExecutionService(connection);
            _authenticationService = new AuthenticationService(_commandExecutionService);
        }

        public Task AuthenticationAsync(string name, string password)
        {
            return _authenticationService.AuthenticationAsync(name, password);
        }

        public Task QuitAsync()
        {
            return _authenticationService.QuitAsync();
        }

        public Task<IApiSentence> ExecuteCommandAsync(IApiCommand command)
        {
            return _commandExecutionService.ExecuteCommandAsync(command);
        }

        public IAsyncEnumerable<IApiSentence> ExecuteCommandToEnumerableAsync(IApiCommand command)
        {
            return _commandExecutionService.ExecuteCommandToEnumerableAsync(command);
        }

        public Task<IReadOnlyList<IApiSentence>> ExecuteCommandToListAsync(IApiCommand command)
        {
            return _commandExecutionService.ExecuteCommandToListAsync(command);
        }

        public IAsyncEnumerable<T> ExecuteCommandToEnumerableAsync<T>(IApiCommand command)
            where T : class, IModelFactory<T>, new()
        {
            return _commandExecutionService.ExecuteCommandToEnumerableAsync<T>(command);
        }

        public Task<IReadOnlyList<T>> ExecuteCommandToListAsync<T>(IApiCommand command)
            where T : class, IModelFactory<T>, new()
        {
            return _commandExecutionService.ExecuteCommandToListAsync<T>(command);
        }
    }
}