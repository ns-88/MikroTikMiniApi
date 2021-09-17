﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MikroTikMiniApi.Exceptions;
using MikroTikMiniApi.Interfaces.Commands;
using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Networking;
using MikroTikMiniApi.Interfaces.Sentences;
using MikroTikMiniApi.Interfaces.Services;
using MikroTikMiniApi.Parameters;
using MikroTikMiniApi.Sentences;
using MikroTikMiniApi.Utilities;

namespace MikroTikMiniApi.Services
{
    internal class CommandExecutionService : ICommandExecutionService
    {
        private readonly IConnection _connection;

        public CommandExecutionService(IConnection connection)
        {
            Guard.ThrowIfNull(connection, out _connection, nameof(connection));
        }

        private static byte[] EncodeWordLength(in long length)
        {
            byte[] array;

            switch (length)
            {
                case < 0x80:
                    array = BitConverter.GetBytes(length);
                    return new[] { array[0] };
                case < 0x4000:
                    array = BitConverter.GetBytes(length | 0x8000);
                    return new[] { array[1], array[0] };
                case < 0x200_000:
                    array = BitConverter.GetBytes(length | 0xC00000);
                    return new[] { array[2], array[1], array[0] };
                case < 0x100_000_000:
                    array = BitConverter.GetBytes(length | 0xE0000000);
                    return new[] { array[3], array[2], array[1], array[0] };
                default:
                    array = BitConverter.GetBytes(length);
                    return new byte[] { 0xF0, array[3], array[2], array[1], array[0] };
            }
        }

        private static async ValueTask<long> ReadWordLengthAsync(IConnection connection)
        {
            var buffer = new Memory<byte>(new byte[5]);

            await connection.ReceiveAsync(buffer.Slice(0, 1)).ConfigureAwait(false);

            if ((buffer.Span[0] & 0x80) == 0x00)
            {
                return buffer.Span[0];
            }

            if ((buffer.Span[0] & 0xC0) == 0x80)
            {
                await connection.ReceiveAsync(buffer.Slice(1, 1)).ConfigureAwait(false);

                return ((buffer.Span[0] & 0x3F) << 8) + buffer.Span[1];
            }

            long length;

            if ((buffer.Span[0] & 0xE0) == 0xC0)
            {
                await connection.ReceiveAsync(buffer.Slice(1, 2)).ConfigureAwait(false);

                length = ((buffer.Span[0] & 0x1F) << 8) + buffer.Span[1];
                length = (length << 8) + buffer.Span[2];
            }
            else
            {
                if ((buffer.Span[0] & 0xF0) == 0xE0)
                {
                    await connection.ReceiveAsync(buffer.Slice(1, 3)).ConfigureAwait(false);

                    length = ((buffer.Span[0] & 0xF) << 8) + buffer.Span[1];
                    length = (length << 8) + buffer.Span[2];
                    length = (length << 8) + buffer.Span[3];
                }
                else
                {
                    await connection.ReceiveAsync(buffer.Slice(1, 4)).ConfigureAwait(false);

                    length = buffer.Span[1];
                    length = (length << 8) + buffer.Span[2];
                    length = (length << 8) + buffer.Span[3];
                    length = (length << 8) + buffer.Span[4];
                }
            }

            return length;
        }

        private static async ValueTask<string> ReadWordAsync(IConnection connection)
        {
            long wordLength;

            try
            {
                wordLength = await ReadWordLengthAsync(connection).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new CommandExecutionFaultException("Значение длины слова API не было получено.", ex);
            }

            if (wordLength <= 0)
                return string.Empty;

            var buffer = new Memory<byte>(new byte[wordLength]);

            await connection.ReceiveAsync(buffer).ConfigureAwait(false);

            return Encoding.ASCII.GetString(buffer.Span);
        }

        async ValueTask ICommandExecutionService.SendCommandAsync(IApiCommand command)
        {
            byte[] buffer;
            var commandArray = GetWordByteArray(command.Text);

            if (command.Parameters.Count != 0)
            {
                var beginIdx = commandArray.Length;
                var parameterArrays = GetParameterByteArrays(command.Parameters, out var totalLength);

                buffer = new byte[commandArray.Length + totalLength + 1];

                Array.Copy(commandArray, buffer, commandArray.Length);

                foreach (var parameterArray in parameterArrays)
                {
                    Array.Copy(parameterArray, 0, buffer, beginIdx, parameterArray.Length);

                    beginIdx += parameterArray.Length;
                }
            }
            else
            {
                buffer = new byte[commandArray.Length + 1];

                Array.Copy(commandArray, buffer, commandArray.Length);
            }

            buffer[^1] = 0;

            var memory = new ReadOnlyMemory<byte>(buffer);

            try
            {
                await _connection.SendAsync(memory).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new CommandExecutionFaultException("Передача команды не была завершена.", ex);
            }

            static IEnumerable<byte[]> GetParameterByteArrays(IReadOnlyList<ApiCommandParameter> parameters, out int totalBytesLength)
            {
                var byteArrays = new byte[parameters.Count][];

                totalBytesLength = 0;

                for (var i = 0; i < parameters.Count; i++)
                {
                    var byteArray = GetWordByteArray(parameters[i]);

                    byteArrays[i] = byteArray;
                    totalBytesLength += byteArray.Length;
                }

                return byteArrays;
            }

            static byte[] GetWordByteArray(string word)
            {
                var wordArray = Encoding.ASCII.GetBytes(word);
                var lengthArray = EncodeWordLength(wordArray.Length);
                var buffer = new byte[lengthArray.Length + wordArray.Length];

                Array.Copy(lengthArray, buffer, lengthArray.Length);
                Array.Copy(wordArray, 0, buffer, lengthArray.Length, wordArray.Length);

                return buffer;
            }
        }

        async ValueTask<IApiSentence> ICommandExecutionService.ReceiveSentenceAsync()
        {
            var words = new List<string>();
            string sentenceName;

            try
            {
                sentenceName = await ReadWordAsync(_connection).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new CommandExecutionFaultException("Наименование типа предложения не было получено.", ex);
            }

            while (true)
            {
                string word;

                try
                {
                    word = await ReadWordAsync(_connection).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw new CommandExecutionFaultException("Значение слова ответа API не было получено.", ex);
                }

                if (string.IsNullOrWhiteSpace(word))
                    break;

                words.Add(word);
            }

            return ApiSentenceBase.Create(sentenceName, words);
        }

        public async Task<IApiSentence> ExecuteCommandAsync(IApiCommand command)
        {
            try
            {
                await ((ICommandExecutionService)this).SendCommandAsync(command).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new CommandExecutionFaultException("Передача команды не была выполнена.", ex);
            }

            try
            {
                return await ((ICommandExecutionService)this).ReceiveSentenceAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new CommandExecutionFaultException("Ответ API не был получен.", ex);
            }
        }

        public IAsyncEnumerable<IApiSentence> ExecuteCommandToEnumerableAsync(IApiCommand command)
        {
            return new SentenceEnumerableNonGeneric(command, this);
        }

        public async Task<IReadOnlyList<IApiSentence>> ExecuteCommandToListAsync(IApiCommand command)
        {
            var list = new List<IApiSentence>();
            var enumerable = new SentenceEnumerableNonGeneric(command, this);

            await foreach (var sentence in enumerable.ConfigureAwait(false))
            {
                list.Add(sentence);
            }

            return list;
        }

        public IAsyncEnumerable<T> ExecuteCommandToEnumerableAsync<T>(IApiCommand command)
            where T : class, IModelFactory<T>, new()
        {
            return new SentenceEnumerableGeneric<T>(command, this, new T());
        }

        public async Task<IReadOnlyList<T>> ExecuteCommandToListAsync<T>(IApiCommand command)
            where T : class, IModelFactory<T>, new()
        {
            var list = new List<T>();
            var enumerable = new SentenceEnumerableGeneric<T>(command, this, new T());

            await foreach (var sentence in enumerable.ConfigureAwait(false))
            {
                list.Add(sentence);
            }

            return list;
        }

        #region Nested types

        private abstract class SentenceEnumerableBase<T> : IAsyncEnumerable<T>
        {
            private readonly IApiCommand _command;
            private readonly ICommandExecutionService _commandExecutionService;

            protected SentenceEnumerableBase(IApiCommand command, ICommandExecutionService commandExecutionService)
            {
                Guard.ThrowIfNull(command, out _command, nameof(command));
                Guard.ThrowIfNull(commandExecutionService, out _commandExecutionService, nameof(commandExecutionService));
            }

            protected async ValueTask SendCommandAsync()
            {
                try
                {
                    await _commandExecutionService.SendCommandAsync(_command).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw new CommandExecutionFaultException("Передача команды не была выполнена.", ex);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            protected async Task<IApiSentence> ReceiveSentenceAsync()
            {
                try
                {
                    return await _commandExecutionService.ReceiveSentenceAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw new CommandExecutionFaultException("Ответ API не был получен.", ex);
                }
            }

            public abstract IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new());
        }

        private class SentenceEnumerableGeneric<T> : SentenceEnumerableBase<T>
            where T : class
        {
            private readonly IModelFactory<T> _modelFactory;

            public SentenceEnumerableGeneric(IApiCommand command, ICommandExecutionService commandExecutionService, IModelFactory<T> modelFactory)
                : base(command, commandExecutionService)
            {
                Guard.ThrowIfNull(modelFactory, out _modelFactory, nameof(modelFactory));
            }

            public override async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                await SendCommandAsync().ConfigureAwait(false);

                while (true)
                {
                    var sentence = await ReceiveSentenceAsync().ConfigureAwait(false);

                    switch (sentence)
                    {
                        case ApiDoneSentence:
                            yield break;
                        case ApiReSentence:
                            yield return _modelFactory.Create(sentence);
                            break;
                        default:
                            {
                                var exceptions = new List<Exception>();

                                try
                                {
                                    await ReceiveSentenceAsync().ConfigureAwait(false);
                                }
                                catch (Exception ex)
                                {
                                    exceptions.Add(ex);
                                }

                                exceptions.Add(new CommandExecutionFaultException($"Получение последовательности не было завершено. Тип ответа API: \"{sentence.GetType().Name}\". Текст ответа: \"{sentence.GetText()}\"."));

                                if (exceptions.Count == 2)
                                    throw new AggregateException(exceptions);

                                throw exceptions[0];
                            }
                    }
                }
            }
        }

        private class SentenceEnumerableNonGeneric : SentenceEnumerableBase<IApiSentence>
        {
            public SentenceEnumerableNonGeneric(IApiCommand command, ICommandExecutionService commandExecutionService)
                : base(command, commandExecutionService)
            {
            }

            public override async IAsyncEnumerator<IApiSentence> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                await SendCommandAsync().ConfigureAwait(false);

                while (true)
                {
                    var sentence = await ReceiveSentenceAsync().ConfigureAwait(false);

                    switch (sentence)
                    {
                        case ApiDoneSentence:
                            yield return sentence;
                            yield break;
                        case ApiReSentence:
                            yield return sentence;
                            break;
                        default:
                            {
                                Exception exception = null;
                                IApiSentence lastSentence = null;

                                yield return sentence;

                                try
                                {
                                    lastSentence = await ReceiveSentenceAsync().ConfigureAwait(false);
                                }
                                catch (Exception ex)
                                {
                                    exception = ex;
                                }

                                if (lastSentence != null)
                                    yield return lastSentence;

                                if (exception == null)
                                    yield break;

                                throw exception;
                            }
                    }
                }
            }
        }

        #endregion
    }
}