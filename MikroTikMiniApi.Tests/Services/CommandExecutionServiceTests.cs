using System;
using System.Threading.Tasks;
using MikroTikMiniApi.Commands;
using MikroTikMiniApi.Sentences;
using MikroTikMiniApi.Services;
using MikroTikMiniApi.Tests.Infrastructure.Networking;
using Xunit;

namespace MikroTikMiniApi.Tests.Services
{
    public class CommandExecutionServiceTests
    {
        [Fact]
        public async Task ExecuteCommandAsync_SendCommandWithoutParameters_Success()
        {
            //Arrange
            var connection = FakeConnectionBase.CreateConnectionSendCommand();
            var service = new CommandExecutionService(connection);
            var command = ApiCommand.New("/system/package/print").Build();

            //Act
            await service.ExecuteCommandAsync(command);

            //Assert
            var memory = new ReadOnlyMemory<byte>(new byte[]
            {
                21, 47, 115, 121, 115, 116, 101, 109, 47, 112, 97,
                99, 107, 97, 103, 101, 47, 112, 114, 105, 110, 116, 0
            });

            Assert.True(memory.Span.SequenceEqual(connection.SendBuffer.Span));
        }

        [Fact]
        public async Task ExecuteCommandAsync_SendCommandWithParameters_Success()
        {
            //Arrange
            var connection = FakeConnectionBase.CreateConnectionSendCommand();
            var service = new CommandExecutionService(connection);
            var command = ApiCommand.New("/login")
                                    .AddParameter("name", "name")
                                    .AddParameter("password", "password")
                                    .Build();

            //Act
            await service.ExecuteCommandAsync(command);

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
        public async Task ExecuteCommandAsync_ReceiveCommand_Success()
        {
            //Arrange
            var connection = FakeConnectionBase.CreateConnectionReceiveCommand();
            var service = new CommandExecutionService(connection);
            var command = ApiCommand.New("/system/package/print").Build();

            //Act
            var actualSentence = await service.ExecuteCommandAsync(command);

            //Assert
            var expectedSentence = new ApiReSentence(new[] { "=.id=*1", "=name=routeros-smips", "=version=6.47.9" });

            Assert.Equal(expectedSentence, actualSentence);
        }
    }
}