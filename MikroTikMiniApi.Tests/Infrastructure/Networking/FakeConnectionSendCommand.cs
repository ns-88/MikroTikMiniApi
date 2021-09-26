using System;
using System.Threading.Tasks;

namespace MikroTikMiniApi.Tests.Infrastructure.Networking
{
    internal class FakeConnectionSendCommand : FakeConnectionBase
    {
        public ReadOnlyMemory<byte> SendBuffer { get; private set; }

        public override ValueTask ReceiveAsync(Memory<byte> buffer)
        {
            switch (InvokeIndex)
            {
                case 0:
                    buffer.Span[0] = SentenceConstants.DoneLength;
                    break;
                case 1:
                    //!done
                    FillBuffer(SentenceConstants.DoneArray, buffer);
                    break;
                case 2:
                    //Completion of the sentence.
                    buffer.Span[0] = 0;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            NextInvoke();

            return ValueTask.CompletedTask;
        }

        public override ValueTask SendAsync(ReadOnlyMemory<byte> buffer)
        {
            SendBuffer = buffer;

            return ValueTask.CompletedTask;
        }
    }
}