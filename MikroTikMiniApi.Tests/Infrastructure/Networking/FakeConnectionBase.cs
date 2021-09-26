using System;
using System.Threading.Tasks;
using MikroTikMiniApi.Interfaces.Networking;

namespace MikroTikMiniApi.Tests.Infrastructure.Networking
{
    internal abstract class FakeConnectionBase : IConnection
    {
        protected int InvokeIndex { get; private set; }

        protected static void FillBuffer(byte[] source, Memory<byte> target)
        {
            if (source.Length != target.Length)
                throw new InvalidOperationException();

            for (var i = 0; i < source.Length; i++)
            {
                target.Span[i] = source[i];
            }
        }

        protected void NextInvoke()
        {
            InvokeIndex++;
        }

        public abstract ValueTask ReceiveAsync(Memory<byte> buffer);

        public abstract ValueTask SendAsync(ReadOnlyMemory<byte> buffer);

        public static FakeConnectionSendCommand CreateForSendCommand()
        {
            return new FakeConnectionSendCommand();
        }

        public static FakeConnectionReceiveCommand CreateForReceiveCommand()
        {
            return new FakeConnectionReceiveCommand();
        }

        public static FakeConnectionQuitAsync CreateForQuitAsync()
        {
            return new FakeConnectionQuitAsync();
        }

        public static FakeConnectionExecuteCommandToListAsync CreateForExecuteCommandToListAsync()
        {
            return new FakeConnectionExecuteCommandToListAsync();
        }

        public static FakeConnectionExecuteCommandToListAsyncFlushStream CreateForExecuteCommandToListAsyncFlushStream()
        {
            return new FakeConnectionExecuteCommandToListAsyncFlushStream();
        }
    }
}