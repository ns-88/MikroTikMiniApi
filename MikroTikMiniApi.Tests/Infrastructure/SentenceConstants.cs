namespace MikroTikMiniApi.Tests.Infrastructure
{
    internal static class SentenceConstants
    {
        public static readonly byte[] DoneArray = { 33, 100, 111, 110, 101 };
        public static readonly byte[] ReArray = { 33, 114, 101 };
        public static readonly byte[] TrapArray = { 33, 116, 114, 97, 112 };
        public static readonly byte[] FatalArray = { 33, 102, 97, 116, 97, 108 };

        public static readonly byte DoneLength = (byte)DoneArray.Length;
        public static readonly byte ReLength = (byte)ReArray.Length;
        public static readonly byte TrapLength = (byte)TrapArray.Length;
        public static readonly byte FatalLength = (byte)FatalArray.Length;
    }
}