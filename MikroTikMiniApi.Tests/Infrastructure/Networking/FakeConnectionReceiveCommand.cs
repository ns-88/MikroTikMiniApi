using System;
using System.Threading.Tasks;

namespace MikroTikMiniApi.Tests.Infrastructure.Networking
{
    internal class FakeConnectionReceiveCommand : FakeConnectionBase
    {
        private int _invokeIndex;

        public override ValueTask ReceiveAsync(Memory<byte> buffer)
        {
            switch (_invokeIndex)
            {
                case 0:
                    //The length of the API response type word.
                    buffer.Span[0] = 3;
                    break;
                case 1:
                    //!re
                    buffer.Span[0] = 33;
                    buffer.Span[1] = 114;
                    buffer.Span[2] = 101;
                    break;
                case 2:
                    //The length of the first word of the sentence.
                    buffer.Span[0] = 7;
                    break;
                case 3:
                    //=.id=*1
                    buffer.Span[0] = 61;
                    buffer.Span[1] = 46;
                    buffer.Span[2] = 105;
                    buffer.Span[3] = 100;
                    buffer.Span[4] = 61;
                    buffer.Span[5] = 42;
                    buffer.Span[6] = 49;
                    break;
                case 4:
                    //The length of the second word of the sentence.
                    buffer.Span[0] = 20;
                    break;
                case 5:
                    //=name=routeros-smips
                    buffer.Span[0] = 61;
                    buffer.Span[1] = 110;
                    buffer.Span[2] = 97;
                    buffer.Span[3] = 109;
                    buffer.Span[4] = 101;
                    buffer.Span[5] = 61;
                    buffer.Span[6] = 114;
                    buffer.Span[7] = 111;
                    buffer.Span[8] = 117;
                    buffer.Span[9] = 116;
                    buffer.Span[10] = 101;
                    buffer.Span[11] = 114;
                    buffer.Span[12] = 111;
                    buffer.Span[13] = 115;
                    buffer.Span[14] = 45;
                    buffer.Span[15] = 115;
                    buffer.Span[16] = 109;
                    buffer.Span[17] = 105;
                    buffer.Span[18] = 112;
                    buffer.Span[19] = 115;
                    break;
                case 6:
                    //The length of the third word of the sentence.
                    buffer.Span[0] = 15;
                    break;
                case 7:
                    //=version=6.47.9
                    buffer.Span[0] = 61;
                    buffer.Span[1] = 118;
                    buffer.Span[2] = 101;
                    buffer.Span[3] = 114;
                    buffer.Span[4] = 115;
                    buffer.Span[5] = 105;
                    buffer.Span[6] = 111;
                    buffer.Span[7] = 110;
                    buffer.Span[8] = 61;
                    buffer.Span[9] = 54;
                    buffer.Span[10] = 46;
                    buffer.Span[11] = 52;
                    buffer.Span[12] = 55;
                    buffer.Span[13] = 46;
                    buffer.Span[14] = 57;
                    break;
                case 8:
                    //Completion of the sentence.
                    buffer.Span[0] = 0;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            _invokeIndex++;

            return ValueTask.CompletedTask;
        }

        public override ValueTask SendAsync(ReadOnlyMemory<byte> buffer)
        {
            return ValueTask.CompletedTask;
        }
    }
}