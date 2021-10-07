using System;
using System.Runtime.CompilerServices;
using MikroTikMiniApi.Exceptions;
using MikroTikMiniApi.Interfaces.Sentences;
using MikroTikMiniApi.Resources;

namespace MikroTikMiniApi.Models.Api
{
    public abstract class ModelBase
    {
        public string? Id { get; protected set; }

        private static Exception GetException<TExpectedType>(string name, string value)
        {
            return new ReceivingModelValueFaultException(string.Format(Strings.ModelValueNotReceived, typeof(TExpectedType).Name, name, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static string? GetStringValueOrDefault(string name, IApiSentence sentence)
        {
            return sentence.TryGetWordValue(name, out var value)
                ? value
                : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static int? GetIntValueOrDefault(string name, IApiSentence sentence)
        {
            if (!sentence.TryGetWordValue(name, out var textValue))
                return 0;
            
            if (int.TryParse(textValue, out var value))
                return value;

            throw GetException<int>(name, textValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static bool? GetBoolValueOrDefault(string name, IApiSentence sentence)
        {
            if (!sentence.TryGetWordValue(name, out var textValue))
                return null;

            if (bool.TryParse(textValue, out var value))
                return value;

            throw GetException<bool>(name, textValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static ulong? GetUlongValueOrDefault(string name, IApiSentence sentence)
        {
            if (!sentence.TryGetWordValue(name, out var textValue))
                return null;
            
            if (ulong.TryParse(textValue, out var value))
                return value;

            throw GetException<ulong>(name, textValue);
        }
    }
}