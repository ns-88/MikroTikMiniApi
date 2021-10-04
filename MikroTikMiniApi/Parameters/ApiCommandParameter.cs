using MikroTikMiniApi.Utilities;

namespace MikroTikMiniApi.Parameters
{
    /// <summary>
    /// Command parameter.
    /// </summary>
    public class ApiCommandParameter
    {
        private readonly string _text;

        /// <summary>
        /// Creates a new parameter with some arbitrary text.
        /// </summary>
        /// <param name="text">Arbitrary text.</param>
        internal ApiCommandParameter(string text)
        {
            Guard.ThrowIfEmptyString(text, out _text, nameof(text));
        }

        /// <summary>
        /// Creates a new parameter with a name and value.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        internal ApiCommandParameter(string name, string value)
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