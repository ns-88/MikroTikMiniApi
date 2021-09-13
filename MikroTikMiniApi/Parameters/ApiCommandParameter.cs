using MikroTikMiniApi.Utilities;

namespace MikroTikMiniApi.Parameters
{
    public class ApiCommandParameter
    {
        private readonly string _text;

        public ApiCommandParameter(string text)
        {
            Guard.ThrowIfEmptyString(text, out _text, nameof(text));
        }

        public ApiCommandParameter(string name, string value)
        {
            Guard.ThrowIfEmptyString(name, nameof(name));
            Guard.ThrowIfEmptyString(value, nameof(value));

            _text = $"={name}={value}";
        }

        public static implicit operator string(ApiCommandParameter parameter)
        {
            return parameter._text;
        }
    }
}