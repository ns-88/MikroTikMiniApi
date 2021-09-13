using MikroTikMiniApi.Utilities;

namespace MikroTikMiniApi.Parameters
{
    public class ApiCommandParameter
    {
        public readonly string Name;
        public readonly string Value;

        public ApiCommandParameter(string name, string value)
        {
            Guard.ThrowIfEmptyString(name, out Name, nameof(name));
            Guard.ThrowIfEmptyString(value, out Value, nameof(value));
        }

        public override string ToString()
        {
            return $"{Name}={Value}";
        }
    }
}