using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MikroTikMiniApi.Commands;
using MikroTikMiniApi.Interfaces.Sentences;
using MikroTikMiniApi.Models.Api;
using MikroTikMiniApi.Models.Settings;
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
            var connection = FakeConnectionBase.CreateForSendCommand();
            var service = new CommandExecutionService(connection);
            var command = ApiCommand.New("/system/package/print").Build();

            //Act
            await service.ExecuteCommandAsync(command, null);

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
            var connection = FakeConnectionBase.CreateForSendCommand();
            var service = new CommandExecutionService(connection);
            var command = ApiCommand.New("/login")
                                    .AddParameter("name", "name")
                                    .AddParameter("password", "password")
                                    .Build();

            //Act
            await service.ExecuteCommandAsync(command, null);

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
        public async Task ExecuteCommandAsync_ReceiveResponse_Success()
        {
            //Arrange
            var connection = FakeConnectionBase.CreateForReceiveCommand();
            var service = new CommandExecutionService(connection);
            var command = ApiCommand.New("/system/package/print").Build();

            //Act
            var actualSentence = await service.ExecuteCommandAsync(command, null);

            //Assert
            var expectedSentence = new ApiReSentence(new[] { "=.id=*1", "=name=routeros-smips", "=version=6.47.9" });

            Assert.Equal(expectedSentence, actualSentence);
        }

        [Fact]
        public async Task ExecuteCommandToListAsync_SendCommand_Success()
        {
            //Arrange
            var connection = FakeConnectionBase.CreateForSendCommand();
            var service = new CommandExecutionService(connection);
            var command = ApiCommand.New("/ip/service/print").Build();

            //Act
            await service.ExecuteCommandToListAsync(command, null);

            //Assert
            var memory = new ReadOnlyMemory<byte>(new byte[]
            {
                17, 47, 105, 112, 47, 115, 101,
                114, 118, 105, 99, 101, 47, 112,
                114, 105, 110, 116, 0
            });

            Assert.True(memory.Span.SequenceEqual(connection.SendBuffer.Span));
        }

        [Fact]
        public async Task ExecuteCommandToListAsync_ReceiveResponse_Success()
        {
            //Arrange
            var connection = FakeConnectionBase.CreateForExecuteCommandToListAsync();
            var service = new CommandExecutionService(connection);
            var command = ApiCommand.New("/ip/service/print").Build();

            //Act
            var actualList = await service.ExecuteCommandToListAsync(command, null);

            //Assert
            var expectedList = new IApiSentence[]
            {
                new ApiReSentence(new List<string>{"=.id=*0", "=name=telnet", "=port=23", "=address=", "=invalid=true", "=disabled=true"}),
                new ApiDoneSentence(new List<string>())
            };

            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public async Task ExecuteCommandToListAsyncGeneric_ReceiveResponse_Success()
        {
            //Arrange
            var connection = FakeConnectionBase.CreateForExecuteCommandToListAsync();
            var service = new CommandExecutionService(connection);
            var command = ApiCommand.New("/ip/service/print").Build();

            //Act
            var actualList = await service.ExecuteCommandToListAsync<Service>(command, null);

            //Assert
            Assert.True(actualList[0].Id == "*0" &&
                        actualList[0].Name == "telnet" &&
                        actualList[0].Port == 23 &&
                        actualList[0].Address == null &&
                        actualList[0].IsInvalid == true &&
                        actualList[0].IsDisabled == true);
        }

        [Fact]
        public async Task ExecuteCommandToListAsync_FlushStreamDefaultSettings_Success()
        {
            //Arrange
            var connection = FakeConnectionBase.CreateForExecuteCommandToListAsyncFlushStream();
            var service = new CommandExecutionService(connection);
            var command = ApiCommand.New("/interface/print1").Build();

            //Act
            var actualList = await service.ExecuteCommandToListAsync(command, null);

            //Assert
            var expectedList = new IApiSentence[]
            {
                new ApiTrapSentence(new List<string>()),
                new ApiTrapSentence(new List<string>()),
                new ApiDoneSentence(new List<string>())
            };

            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public async Task ExecuteCommandToListAsync_FlushStreamNonDefaultSettings_Success()
        {
            //Arrange
            var connection = FakeConnectionBase.CreateForExecuteCommandToListAsyncFlushStream();
            var service = new CommandExecutionService(connection);
            var command = ApiCommand.New("/interface/print1").Build();

            //Act
            var actualList = await service.ExecuteCommandToListAsync(command, ExecutionSettings.Default.Builder.WithAttemptsCount(3).Build());

            //Assert
            var expectedList = new IApiSentence[]
            {
                new ApiTrapSentence(new List<string>()),
                new ApiTrapSentence(new List<string>()),
                new ApiDoneSentence(new List<string>())
            };

            Assert.Equal(expectedList, actualList);
        }
    }
}