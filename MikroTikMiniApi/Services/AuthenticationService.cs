using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MikroTikMiniApi.Commands;
using MikroTikMiniApi.Exceptions;
using MikroTikMiniApi.Interfaces.Commands;
using MikroTikMiniApi.Interfaces.Sentences;
using MikroTikMiniApi.Interfaces.Services;
using MikroTikMiniApi.Sentences;
using MikroTikMiniApi.Utilities;

namespace MikroTikMiniApi.Services
{
    using ILocalizationService = IAuthenticationLocalizationService;

    ///<inheritdoc cref="IAuthenticationService"/>
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly ICommandExecutionService _commandExecutionService;
        private readonly ILocalizationService _localization;

        public AuthenticationService(ICommandExecutionService commandExecutionService, ILocalizationService localizationService)
        {
            Guard.ThrowIfNull(commandExecutionService, out _commandExecutionService, nameof(commandExecutionService));
            Guard.ThrowIfNull(localizationService, out _localization, nameof(localizationService));
        }

        private static string EncodePassword(string password, string hash)
        {
            var hashByteArray = new byte[hash.Length / 2];
            var hashSpan = hash.AsSpan();

            for (var i = 0; i <= hash.Length - 2; i += 2)
            {
                hashByteArray[i / 2] = byte.Parse(hashSpan.Slice(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            var passwordByteArray = Encoding.ASCII.GetBytes(password);
            var buffer = new byte[passwordByteArray.Length + hashByteArray.Length + 1];

            buffer[0] = 0;

            Array.Copy(passwordByteArray, 0, buffer, 1, passwordByteArray.Length);
            Array.Copy(hashByteArray, 0, buffer, passwordByteArray.Length + 1, hashByteArray.Length);

            byte[] hashed;

            using (var md5 = MD5.Create())
            {
                hashed = md5.ComputeHash(buffer);
            }

            var builder = new StringBuilder("00");

            foreach (var b in hashed)
            {
                builder.AppendFormat("{0:x2}", b);
            }

            return builder.ToString();
        }

        private async Task<IApiSentence> ExecuteCommandAsync(IApiCommand command, string errorMessage)
        {
            try
            {
                return await _commandExecutionService.ExecuteCommandAsync(command).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new CommandExecutionFaultException(errorMessage, ex);
            }
        }

        ///<inheritdoc/>
        public async Task AuthenticationAsync(string name, string password)
        {
            Guard.ThrowIfEmptyString(name, nameof(name));
            Guard.ThrowIfEmptyString(password, nameof(password));

            var errorMessage = _localization.GetAuthCmdFailedText();
            var authCommand = ApiCommand.New("/login")
                                        .AddParameter("name", name)
                                        .AddParameter("password", password)
                                        .Build();

            var sentence = await ExecuteCommandAsync(authCommand, errorMessage).ConfigureAwait(false);

            ThrowIfSentenceIsNotDone(sentence, _localization);

            if (!sentence.TryGetWordValue("ret", out var retValue))
            {
                ThrowIfSentenceIsNotEmpty(sentence, _localization);

                return;
            }

            var hashedPassword = EncodePassword(password, retValue);
            var oldAuthCommand = ApiCommand.New("/login")
                                           .AddParameter("name", name)
                                           .AddParameter("response", hashedPassword)
                                           .Build();

            sentence = await ExecuteCommandAsync(oldAuthCommand, errorMessage).ConfigureAwait(false);
            
            ThrowIfSentenceIsNotDone(sentence, _localization);
            ThrowIfSentenceIsNotEmpty(sentence, _localization);

            static void ThrowIfSentenceIsNotDone(IApiSentence sentence, ILocalizationService localization)
            {
                if (sentence is not ApiDoneSentence)
                    throw new AuthenticationFaultException(localization.GetAuthFailedText(sentence, sentence.GetText()));
            }

            static void ThrowIfSentenceIsNotEmpty(IApiSentence sentence, ILocalizationService localization)
            {
                if (sentence.Words.Count != 0)
                    throw new InvalidOperationException(localization.GetAuthFailedIncorrectAnswerText(sentence));
            }
        }

        ///<inheritdoc/>
        public async Task QuitAsync()
        {
            var command = ApiCommand.New("/quit").Build();
            var sentence = await ExecuteCommandAsync(command, _localization.GetLogoutCmdFailedText()).ConfigureAwait(false);

            if (!(sentence is ApiFatalSentence &&
                  sentence.Words.Count == 1 &&
                  sentence.Words[0].Equals("session terminated on request", StringComparison.OrdinalIgnoreCase)))
            {
                throw new AuthenticationFaultException(_localization.GetLogoutFailedText(sentence, sentence.GetText()));
            }
        }
    }
}