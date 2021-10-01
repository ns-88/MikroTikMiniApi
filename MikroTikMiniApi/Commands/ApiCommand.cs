using System.Collections.Generic;
using MikroTikMiniApi.Interfaces.Commands;
using MikroTikMiniApi.Parameters;
using MikroTikMiniApi.Utilities;

namespace MikroTikMiniApi.Commands
{
    public class ApiCommand : IApiCommand
    {
        private readonly List<ApiCommandParameter> _parameters;

        public string Text { get; }
        public IReadOnlyList<ApiCommandParameter> Parameters { get; }

        private ApiCommand(string value)
        {
            Guard.ThrowIfEmptyString(value, nameof(value));

            _parameters = new List<ApiCommandParameter>();

            Parameters = _parameters;
            Text = value;
        }

        #region Builder

        public static ApiCommandBuilder New(string value)
        {
            return new ApiCommandBuilder(value);
        }

        public class ApiCommandBuilder
        {
            private readonly ApiCommand _command;

            public ApiCommandBuilder(string value)
            {
                _command = new ApiCommand(value);
            }

            public ApiCommandBuilder AddParameter(string name, string value)
            {
                _command._parameters.Add(new ApiCommandParameter(name, value));
                return this;
            }

            public ApiCommandBuilder AddParameter(string text)
            {
                _command._parameters.Add(new ApiCommandParameter(text));
                return this;
            }

            public IApiCommand Build()
            {
                return _command;
            }
        }

        #endregion
    }
}