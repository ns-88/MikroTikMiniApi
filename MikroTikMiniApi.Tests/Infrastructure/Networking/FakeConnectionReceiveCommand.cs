using System;
using System.Threading.Tasks;

namespace MikroTikMiniApi.Tests.Infrastructure.Networking
{
    internal class FakeConnectionReceiveCommand : FakeConnectionBase
    {
        public override ValueTask ReceiveAsync(Memory<byte> buffer)
        {
            byte[] array;

            switch (InvokeIndex)
            {
                case 0:
                    //The length of the API response type word.
                    buffer.Span[0] = 3;
                    break;
                case 1:
                    //!re
                    FillBuffer(new byte[] { 33, 114, 101 }, buffer);
                    break;
                case 2:
                    //The length of the first word of the sentence.
                    buffer.Span[0] = 7;
                    break;
                case 3:
                    //=.id=*1
                    FillBuffer(new byte[] { 61, 46, 105, 100, 61, 42, 49 }, buffer);
                    break;
                case 4:
                    //The length of the second word of the sentence.
                    buffer.Span[0] = 20;
                    break;
                case 5:
                    //=name=routeros-smips
                    array = new byte[]
                    {
                        61, 110, 97, 109, 101, 61, 114, 111,
                        117, 116, 101, 114, 111, 115, 45,
                        115, 109, 105, 112, 115
                    };

                    FillBuffer(array, buffer);
                    break;
                case 6:
                    //The length of the third word of the sentence.
                    buffer.Span[0] = 15;
                    break;
                case 7:
                    //=version=6.47.9
                    array = new byte[]
                    {
                        61, 118, 101, 114, 115, 105, 111,
                        110, 61, 54, 46, 52, 55, 46, 57
                    };

                    FillBuffer(array, buffer);
                    break;
                case 8:
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
            return ValueTask.CompletedTask;
        }
    }
}