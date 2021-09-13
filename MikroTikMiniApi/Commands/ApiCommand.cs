using System.Collections.Generic;
using MikroTikMiniApi.Interfaces.Commands;
using MikroTikMiniApi.Parameters;
using MikroTikMiniApi.Utilities;

namespace MikroTikMiniApi.Commands
{
    public class ApiCommand : IApiCommand, IApiCommandBuilder
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

        IApiCommandBuilder IApiCommandBuilder.AddParameter(string name, string value)
        {
            _parameters.Add(new ApiCommandParameter(name, value));

            return this;
        }

        IApiCommandBuilder IApiCommandBuilder.AddParameter(string text)
        {
            _parameters.Add(new ApiCommandParameter(text));

            return this;
        }

        IApiCommand IApiCommandBuilder.Build()
        {
            return this;
        }

        public static IApiCommandBuilder New(string value)
        {
            return new ApiCommand(value);
        }
    }
}