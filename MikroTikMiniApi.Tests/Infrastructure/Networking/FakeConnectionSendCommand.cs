using System;
using System.Threading.Tasks;

namespace MikroTikMiniApi.Tests.Infrastructure.Networking
{
    internal class FakeConnectionSendCommand : FakeConnectionBase
    {
        private int _invokeIndex;
        public ReadOnlyMemory<byte> SendBuffer { get; private set; }

        public override ValueTask ReceiveAsync(Memory<byte> buffer)
        {
            switch (_invokeIndex)
            {
                case 0:
                    buffer.Span[0] = 5;
                    break;
                case 1:
                    buffer.Span[0] = 33;
                    buffer.Span[1] = 100;
                    buffer.Span[2] = 111;
                    buffer.Span[3] = 110;
                    buffer.Span[4] = 101;
                    break;
                case 2:
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
            SendBuffer = buffer;

            return ValueTask.CompletedTask;
        }
    }
}