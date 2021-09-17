using System;
using System.Threading.Tasks;
using MikroTikMiniApi.Exceptions;
using MikroTikMiniApi.Services;
using MikroTikMiniApi.Tests.Infrastructure.Networking;
using Xunit;

namespace MikroTikMiniApi.Tests.Services
{
    public class AuthenticationServiceTests
    {
        [Fact]
        public async Task AuthenticationAsync_SendCommand_Success()
        {
            //Arrange
            var connection = FakeConnectionBase.CreateConnectionSendCommand();
            var service = new AuthenticationService(new CommandExecutionService(connection));

            //Act
            await service.AuthenticationAsync("name", "password");

            //Assert
            var memory = new ReadOnlyMemory<byte>(new byte[]
            {
                6, 47, 108, 111, 103, 105, 110, 10, 61, 110,
                97, 109, 101, 61, 110, 97, 109, 101, 18, 61,
                112, 97, 115, 115, 119, 111, 114, 100, 61, 112,
                97, 115, 115, 119, 111, 114, 100, 0
            });

            Assert.True(memory.Span.SequenceEqual(connection.SendBuffer.Span));
        }

        [Fact]
        public async Task AuthenticationAsync_ReceiveResponse_Success()
        {
            //Arrange
            var connection = FakeConnectionBase.CreateConnectionSendCommand();
            var service = new AuthenticationService(new CommandExecutionService(connection));

            //Act
            var exception = await Record.ExceptionAsync(() => service.AuthenticationAsync("name", "password"));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task QuitAsync_SendCommand_Success()
        {
            //Arrange
            var connection = FakeConnectionBase.CreateConnectionSendCommand();
            var service = new AuthenticationService(new CommandExecutionService(connection));

            //Act
            var exception = await Record.ExceptionAsync(() => service.QuitAsync());

            //Assert
            var memory = new ReadOnlyMemory<byte>(new byte[] { 5, 47, 113, 117, 105, 116, 0 });

            Assert.IsType<AuthenticationFaultException>(exception);
            Assert.True(memory.Span.SequenceEqual(connection.SendBuffer.Span));
        }

        [Fact]
        public async Task QuitAsync_ReceiveResponse_Success()
        {
            //Arrange
            var connection = FakeConnectionBase.CreateConnectionQuitAsync();
            var service = new AuthenticationService(new CommandExecutionService(connection));

            //Act
            var exception = await Record.ExceptionAsync(() => service.QuitAsync());

            //Assert
            Assert.Null(exception);
        }
    }
}