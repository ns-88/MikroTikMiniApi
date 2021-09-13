using System;
using System.Threading.Tasks;

namespace MikroTikMiniApi.Tests.Infrastructure.Networking
{
    internal class FakeConnectionQuitAsyncReceiveResponse : FakeConnectionBase
    {
        public override ValueTask ReceiveAsync(Memory<byte> buffer)
        {
            switch (InvokeIndex)
            {
                case 0:
                    //The length of the API response type word.
                    buffer.Span[0] = 6;
                    break;
                case 1:
                    //!fatal
                    FillBuffer(new byte[] { 33, 102, 97, 116, 97, 108 }, buffer);
                    break;
                case 2:
                    //The length of the first word of the sentence.
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