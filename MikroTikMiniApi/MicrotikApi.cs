using System.Collections.Generic;
using System.Threading.Tasks;
using MikroTikMiniApi.Interfaces;
using MikroTikMiniApi.Interfaces.Commands;
using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Models.Settings;
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

        public Task<IApiSentence> ExecuteCommandAsync(IApiCommand command, IExecutionSettings settings)
        {
            return _commandExecutionService.ExecuteCommandAsync(command, settings);
        }

        public IAsyncEnumerable<IApiSentence> ExecuteCommandToEnumerableAsync(IApiCommand command, IExecutionSettings settings)
        {
            return _commandExecutionService.ExecuteCommandToEnumerableAsync(command, settings);
        }

        public Task<IReadOnlyList<IApiSentence>> ExecuteCommandToListAsync(IApiCommand command, IExecutionSettings settings)
        {
            return _commandExecutionService.ExecuteCommandToListAsync(command, settings);
        }

        public IAsyncEnumerable<T> ExecuteCommandToEnumerableAsync<T>(IApiCommand command, IExecutionSettings settings)
            where T : class, IModelFactory<T>, new()
        {
            return _commandExecutionService.ExecuteCommandToEnumerableAsync<T>(command, settings);
        }

        public Task<IReadOnlyList<T>> ExecuteCommandToListAsync<T>(IApiCommand command, IExecutionSettings settings)
            where T : class, IModelFactory<T>, new()
        {
            return _commandExecutionService.ExecuteCommandToListAsync<T>(command, settings);
        }
    }
}