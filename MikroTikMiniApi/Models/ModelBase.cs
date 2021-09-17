using System;
using System.Runtime.CompilerServices;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Models
{
    public abstract class ModelBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static string GetStringValue(string name, IApiSentence sentence)
        {
            return sentence.TryGetWordValue(name, out var value)
                ? value
                : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static int GetIntValue(string name, IApiSentence sentence)
        {
            if (!sentence.TryGetWordValue(name, out var textValue))
                return 0;

            if (int.TryParse(textValue, out var value))
                return value;

            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static bool? GetBoolValue(string name, IApiSentence sentence)
        {
            if (!sentence.TryGetWordValue(name, out var textValue))
                return null;

            if (bool.TryParse(textValue, out var value))
                return value;

            throw new InvalidOperationException();
        }
    }
}