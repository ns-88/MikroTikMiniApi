using System.Collections.Generic;
using MikroTikMiniApi.Parameters;

namespace MikroTikMiniApi.Interfaces.Commands
{
    public interface IApiCommand
    {
        public string Text { get; }
        IReadOnlyList<ApiCommandParameter> Parameters { get; }
    }
}