﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MikroTikMiniApi.Interfaces.Commands;
using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Interfaces
{
    public interface IRouterApi
    {
        Task AuthenticationAsync(string name, string password);

        Task QuitAsync();

        Task<IApiSentence> ExecuteCommandAsync(IApiCommand command);

        IAsyncEnumerable<IApiSentence> ExecuteCommandToEnumerableAsync(IApiCommand command);

        Task<IReadOnlyList<IApiSentence>> ExecuteCommandToListAsync(IApiCommand command);

        IAsyncEnumerable<T> ExecuteCommandToEnumerableAsync<T>(IApiCommand command)
            where T : class, IModelFactory<T>, new();

        Task<IReadOnlyList<T>> ExecuteCommandToListAsync<T>(IApiCommand command)
            where T : class, IModelFactory<T>, new();
    }
}