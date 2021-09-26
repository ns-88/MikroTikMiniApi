using System;
using System.Threading.Tasks;

namespace MikroTikMiniApi.Tests.Infrastructure.Networking
{
    internal class FakeConnectionExecuteCommandToListAsync : FakeConnectionBase
    {
        public override ValueTask ReceiveAsync(Memory<byte> buffer)
        {
            switch (InvokeIndex)
            {
                case 0:
                    buffer.Span[0] = SentenceConstants.ReLength;
                    break;
                case 1:
                    //!re
                    FillBuffer(SentenceConstants.ReArray, buffer);
                    break;
                case 2:
                    buffer.Span[0] = 7;
                    break;
                case 3:
                    //=.id=*0
                    FillBuffer(new byte[] { 61, 46, 105, 100, 61, 42, 48, }, buffer);
                    break;
                case 4:
                    buffer.Span[0] = 12;
                    break;
                case 5:
                    //=name=telnet
                    FillBuffer(new byte[] { 61, 110, 97, 109, 101, 61, 116, 101, 108, 110, 101, 116 }, buffer);
                    break;
                case 6:
                    buffer.Span[0] = 8;
                    break;
                case 7:
                    //=port=23
                    FillBuffer(new byte[]{ 61, 112, 111, 114, 116, 61, 50, 51 }, buffer);
                    break;
                case 8:
                    buffer.Span[0] = 9;
                    break;
                case 9:
                    //=address=
                    FillBuffer(new byte[]{ 61, 97, 100, 100, 114, 101, 115, 115, 61 }, buffer);
                    break;
                case 10:
                    buffer.Span[0] = 13;
                    break;
                case 11:
                    //=invalid=true
                    FillBuffer(new byte[]{ 61, 105, 110, 118, 97, 108, 105, 100, 61, 116, 114, 117, 101 }, buffer);
                    break;
                case 12:
                    buffer.Span[0] = 14;
                    break;
                case 13:
                    //=disabled=true
                    FillBuffer(new byte[] { 61, 100, 105, 115, 97, 98, 108, 101, 100, 61, 116, 114, 117, 101 }, buffer);
                    break;
                case 14:
                    //Completion of the !re sentence.
                    buffer.Span[0] = 0;
                    break;
                case 15:
                    buffer.Span[0] = SentenceConstants.DoneLength;
                    break;
                case 16:
                    //!done
                    FillBuffer(SentenceConstants.DoneArray, buffer);
                    break;
                case 17:
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