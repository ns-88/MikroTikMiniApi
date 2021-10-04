using System.Collections.Generic;
using System.Threading.Tasks;
using MikroTikMiniApi.Interfaces.Commands;
using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Models.Settings;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Interfaces
{
    /// <summary>
    /// API for interacting with the router - executing commands and receiving data.
    /// </summary>
    public interface IRouterApi
    {
        #region Comment
        /// <inheritdoc cref="Services.IAuthenticationService.AuthenticationAsync"/>
        #endregion
        Task AuthenticationAsync(string name, string password);

        #region Comment
        /// <inheritdoc cref="Services.IAuthenticationService.QuitAsync"/>
        #endregion
        Task QuitAsync();

        #region Comment
        /// <inheritdoc cref="Services.ICommandExecutionService.ExecuteCommandAsync"/>
        #endregion
        Task<IApiSentence> ExecuteCommandAsync(IApiCommand command, IExecutionSettings settings = null);

        #region Comment
        /// <inheritdoc cref="Services.ICommandExecutionService.ExecuteCommandToEnumerableAsync"/>
        #endregion
        IAsyncEnumerable<IApiSentence> ExecuteCommandToEnumerableAsync(IApiCommand command, IExecutionSettings settings = null);

        #region Comment
        /// <inheritdoc cref="Services.ICommandExecutionService.ExecuteCommandToListAsync"/>
        #endregion
        Task<IReadOnlyList<IApiSentence>> ExecuteCommandToListAsync(IApiCommand command, IExecutionSettings settings = null);

        #region Comment
        /// <inheritdoc cref="Services.ICommandExecutionService.ExecuteCommandToEnumerableAsync"/>
        #endregion
        IAsyncEnumerable<T> ExecuteCommandToEnumerableAsync<T>(IApiCommand command, IExecutionSettings settings = null)
            where T : class, IModelFactory<T>, new();

        #region Comment
        /// <inheritdoc cref="Services.ICommandExecutionService.ExecuteCommandToListAsync"/>
        #endregion
        Task<IReadOnlyList<T>> ExecuteCommandToListAsync<T>(IApiCommand command, IExecutionSettings settings = null)
            where T : class, IModelFactory<T>, new();
    }
}