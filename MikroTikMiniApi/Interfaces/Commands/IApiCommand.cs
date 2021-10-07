using System.Collections.Generic;
using MikroTikMiniApi.Parameters;

namespace MikroTikMiniApi.Interfaces.Commands
{
    /// <summary>
    /// API command.
    /// </summary>
    public interface IApiCommand
    {
        /// <summary>
        /// The text of the command. Must be specified.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Command parameters. May not be specified.
        /// </summary>
        IReadOnlyList<ApiCommandParameter> Parameters { get; }
    }
}