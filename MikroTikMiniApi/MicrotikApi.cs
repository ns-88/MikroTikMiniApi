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

namespace MikroTikMiniApi
{
    ///<inheritdoc cref="IRouterApi"/>
    internal class MicrotikApi : IRouterApi
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICommandExecutionService _commandExecutionService;

        public MicrotikApi(IConnection connection, ILocalizationService localizationService, IApiSentenceFactory sentenceFactory)
        {
            _commandExecutionService = new CommandExecutionService(connection, localizationService, sentenceFactory);
            _authenticationService = new AuthenticationService(_commandExecutionService, localizationService);
        }

        ///<inheritdoc/>
        public Task AuthenticationAsync(string name, string password)
        {
            return _authenticationService.AuthenticationAsync(name, password);
        }

        ///<inheritdoc/>
        public Task QuitAsync()
        {
            return _authenticationService.QuitAsync();
        }

        ///<inheritdoc/>
        public Task<IApiSentence> ExecuteCommandAsync(IApiCommand command, IExecutionSettings settings)
        {
            return _commandExecutionService.ExecuteCommandAsync(command, settings);
        }

        ///<inheritdoc/>
        public IAsyncEnumerable<IApiSentence> ExecuteCommandToEnumerableAsync(IApiCommand command, IExecutionSettings settings)
        {
            return _commandExecutionService.ExecuteCommandToEnumerableAsync(command, settings);
        }

        ///<inheritdoc/>
        public Task<IReadOnlyList<IApiSentence>> ExecuteCommandToListAsync(IApiCommand command, IExecutionSettings settings)
        {
            return _commandExecutionService.ExecuteCommandToListAsync(command, settings);
        }

        ///<inheritdoc/>
        public IAsyncEnumerable<T> ExecuteCommandToEnumerableAsync<T>(IApiCommand command, IExecutionSettings settings)
            where T : class, IModelFactory<T>, new()
        {
            return _commandExecutionService.ExecuteCommandToEnumerableAsync<T>(command, settings);
        }

        ///<inheritdoc/>
        public Task<IReadOnlyList<T>> ExecuteCommandToListAsync<T>(IApiCommand command, IExecutionSettings settings)
            where T : class, IModelFactory<T>, new()
        {
            return _commandExecutionService.ExecuteCommandToListAsync<T>(command, settings);
        }
    }
}