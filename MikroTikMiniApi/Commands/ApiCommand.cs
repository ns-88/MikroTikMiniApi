using System.Collections.Generic;
using MikroTikMiniApi.Interfaces.Commands;
using MikroTikMiniApi.Parameters;
using MikroTikMiniApi.Utilities;

namespace MikroTikMiniApi.Commands
{
    ///<inheritdoc cref="IApiCommand"/>
    public class ApiCommand : IApiCommand
    {
        private readonly List<ApiCommandParameter> _parameters;

        ///<inheritdoc/>
        public string Text { get; }

        ///<inheritdoc/>
        public IReadOnlyList<ApiCommandParameter> Parameters { get; }

        private ApiCommand(string text)
        {
            Guard.ThrowIfEmptyString(text, nameof(text));

            _parameters = new List<ApiCommandParameter>();

            Parameters = _parameters;
            Text = text;
        }

        #region Builder

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="text">Command text.</param>
        /// <returns>Command builder.</returns>
        public static ApiCommandBuilder New(string text)
        {
            return new ApiCommandBuilder(text);
        }

        /// <summary>
        /// Command builder.
        /// </summary>
        public class ApiCommandBuilder
        {
            private readonly ApiCommand _command;

            internal ApiCommandBuilder(string text)
            {
                _command = new ApiCommand(text);
            }

            /// <inheritdoc cref="ApiCommandParameter(string)"/>
            public ApiCommandBuilder AddParameter(string text)
            {
                _command._parameters.Add(new ApiCommandParameter(text));
                return this;
            }

            /// <inheritdoc cref="ApiCommandParameter(string, string)"/>
            public ApiCommandBuilder AddParameter(string name, string value)
            {
                _command._parameters.Add(new ApiCommandParameter(name, value));
                return this;
            }

            /// <summary>
            /// Returns a ready-to-use command.
            /// </summary>
            /// <returns>Command.</returns>
            public IApiCommand Build()
            {
                return _command;
            }
        }

        #endregion
    }
}