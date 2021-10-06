using System.Collections.Generic;
using System.Threading.Tasks;
using MikroTikMiniApi.Interfaces.Commands;
using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Models.Settings;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Interfaces.Services
{
    /// <summary>
    /// Service for executing API commands.
    /// </summary>
    internal interface ICommandExecutionService
    {
        /// <summary>
        /// Sends an API command to the router.
        /// </summary>
        /// <param name="command">API command.</param>
        /// <returns>A task to wait for.</returns>
        internal ValueTask SendCommandAsync(IApiCommand command);

        /// <summary>
        /// Gets the result of executing the command in the form of a single API sentence.
        /// </summary>
        /// <returns>API sentence.</returns>
        internal ValueTask<IApiSentence> ReceiveSentenceAsync();

        /// <summary>
        /// Clears the router API response stream. Called only if the type of the last API response was ApiTrapSentence.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns>Received sentences from the stream.</returns>
        internal ValueTask<IReadOnlyList<IApiSentence>> FlushResponseStreamAsync(IExecutionSettings settings);

        /// <summary>
        /// Executes the command returning a scalar result.
        /// </summary>
        /// <param name="command">API command.</param>
        /// <param name="settings">Execution settings.</param>
        /// <returns>API sentence.</returns>
        Task<IApiSentence> ExecuteCommandAsync(IApiCommand command, IExecutionSettings settings = null);

        /// <summary>
        /// Executes a command that returns the result as an asynchronous enumerator of elements of the type <see cref="IApiSentence"/>.
        /// </summary>
        /// <param name="command">API command.</param>
        /// <param name="settings">Execution settings.</param>
        /// <returns>An enumerator for receiving command results asynchronously.</returns>
        IAsyncEnumerable<IApiSentence> ExecuteCommandToEnumerableAsync(IApiCommand command, IExecutionSettings settings = null);

        /// <summary>
        /// Executes a command that returns the result as a collection of elements of type <see cref="IApiSentence"/>.
        /// </summary>
        /// <param name="command">API command.</param>
        /// <param name="settings">Execution settings.</param>
        /// <returns>Collection of elements.</returns>
        Task<IReadOnlyList<IApiSentence>> ExecuteCommandToListAsync(IApiCommand command, IExecutionSettings settings = null);

        /// <summary>
        /// Executes a command that returns the result as an asynchronous enumerator of elements of a user-defined type.
        /// </summary>
        /// <typeparam name="T">A data model that is also a factory that creates objects of this type.</typeparam>
        /// <param name="command">API command.</param>
        /// <param name="settings">Execution settings.</param>
        /// <returns>An enumerator for receiving command results asynchronously.</returns>
        IAsyncEnumerable<T> ExecuteCommandToEnumerableAsync<T>(IApiCommand command, IExecutionSettings settings = null)
            where T : class, IModelFactory<T>, new();

        /// <summary>
        /// Executes a command that returns the result as a collection of elements of a user-defined type.
        /// </summary>
        /// <typeparam name="T">A data model that is also a factory that creates objects of this type.</typeparam>
        /// <param name="command">API command.</param>
        /// <param name="settings">Execution settings.</param>
        /// <returns>Collection of elements.</returns>
        Task<IReadOnlyList<T>> ExecuteCommandToListAsync<T>(IApiCommand command, IExecutionSettings settings = null)
            where T : class, IModelFactory<T>, new();
    }
}