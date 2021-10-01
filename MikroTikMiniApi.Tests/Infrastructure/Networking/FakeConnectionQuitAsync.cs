using System;
using System.Threading.Tasks;

namespace MikroTikMiniApi.Tests.Infrastructure.Networking
{
    internal class FakeConnectionQuitAsync : FakeConnectionBase
    {
        public override ValueTask ReceiveAsync(Memory<byte> buffer)
        {
            switch (InvokeIndex)
            {
                case 0:
                    buffer.Span[0] = SentenceConstants.FatalLength;
                    break;
                case 1:
                    //!fatal
                    FillBuffer(SentenceConstants.FatalArray, buffer);
                    break;
                case 2:
                    buffer.Span[0] = 29;
                    break;
                case 3:
                    //session terminated on request
                    var array = new byte[]
                    {
                        115, 101, 115, 115, 105, 111, 110, 32, 116, 101,
                        114, 109, 105, 110, 97, 116, 101, 100, 32,111,
                        110, 32, 114, 101, 113, 117, 101, 115, 116
                    };

                    FillBuffer(array, buffer);
                    break;
                case 4:
                    //Completion of the sentence.
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