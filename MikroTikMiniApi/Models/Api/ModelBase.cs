using System;
using System.Runtime.CompilerServices;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Models.Api
{
    public abstract class ModelBase
    {
        public string Id { get; protected set; }

        private static Exception GetException<TExpectedType>(string name, string value)
        {
            return new InvalidOperationException($"Значение не было получено. Ожидаемый тип: \"{typeof(TExpectedType).Name}\", наименование поля: \"{name}\", значение ответа API: \"{value}\".");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static string GetStringValue(string name, IApiSentence sentence)
        {
            return sentence.TryGetWordValue(name, out var value)
                ? value
                : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static int? GetIntValue(string name, IApiSentence sentence)
        {
            if (!sentence.TryGetWordValue(name, out var textValue))
                return 0;
            
            if (int.TryParse(textValue, out var value))
                return value;

            throw GetException<int>(name, textValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static bool? GetBoolValue(string name, IApiSentence sentence)
        {
            if (!sentence.TryGetWordValue(name, out var textValue))
                return null;

            if (bool.TryParse(textValue, out var value))
                return value;

            throw GetException<bool>(name, textValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static ulong? GetUlongValue(string name, IApiSentence sentence)
        {
            if (!sentence.TryGetWordValue(name, out var textValue))
                return null;
            
            if (ulong.TryParse(textValue, out var value))
                return value;

            throw GetException<ulong>(name, textValue);
        }
    }
}