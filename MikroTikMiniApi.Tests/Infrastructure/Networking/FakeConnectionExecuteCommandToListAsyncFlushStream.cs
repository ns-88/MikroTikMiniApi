using System;
using System.Threading.Tasks;

namespace MikroTikMiniApi.Tests.Infrastructure.Networking
{
    internal class FakeConnectionExecuteCommandToListAsyncFlushStream : FakeConnectionBase
    {
        public override ValueTask ReceiveAsync(Memory<byte> buffer)
        {
            switch (InvokeIndex)
            {
                case 0:
                    buffer.Span[0] = SentenceConstants.TrapLength;
                    break;
                case 1:
                    //!trap
                    FillBuffer(SentenceConstants.TrapArray, buffer);
                    break;
                case 2:
                    //Completion of the !trap sentence.
                    buffer.Span[0] = 0;
                    break;
                case 3:
                    buffer.Span[0] = SentenceConstants.TrapLength;
                    break;
                case 4:
                    //!trap
                    FillBuffer(SentenceConstants.TrapArray, buffer);
                    break;
                case 5:
                    //Completion of the !trap sentence.
                    buffer.Span[0] = 0;
                    break;
                case 6:
                    buffer.Span[0] = SentenceConstants.DoneLength;
                    break;
                case 7:
                    //!done
                    FillBuffer(SentenceConstants.DoneArray, buffer);
                    break;
                case 8:
                    //Completion of the !done sentence.
                    buffer.Span[0] = 0;
                    break;
                default:
                    throw new DataReceivingTestFaultException();
            }

            NextInvoke();

            return ValueTask.CompletedTask;
        }

        public override ValueTask SendAsync(ReadOnlyMemory<byte> buffer)
        {
            return ValueTask.CompletedTask;
        }
    }
}