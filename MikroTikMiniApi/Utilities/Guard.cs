using System;
using System.Runtime.CompilerServices;

namespace MikroTikMiniApi.Utilities
{
    internal static class Guard
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNull<T>(T source, out T target, string paramName) where T : class
        {
            target = source ?? throw new ArgumentNullException(paramName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNull<T>(T source, string paramName) where T : class
        {
            if (source == null)
                throw new ArgumentNullException(paramName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfEmptyString(string source, string paramName)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentException(paramName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfEmptyString(string source, out string target, string paramName)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentException(paramName);

            target = source;
        }
    }
}