using System.Collections.Generic;
using MikroTikMiniApi.Exceptions;
using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Sentences;
using MikroTikMiniApi.Interfaces.Services;
using MikroTikMiniApi.Sentences;
using MikroTikMiniApi.Utilities;

namespace MikroTikMiniApi.Factories
{
    using ILocalizationService = IApiSentenceLocalizationService;
    using static ApiSentenceBase;

    internal class ApiSentenceFactory : IApiSentenceFactory
    {
        private readonly ILocalizationService _localizationService;

        public ApiSentenceFactory(ILocalizationService localizationService)
        {
            Guard.ThrowIfNull(localizationService, out _localizationService, nameof(localizationService));
        }

        IApiSentence IApiSentenceFactory.Create(string sentenceName, IReadOnlyList<string> words)
        {
            Guard.ThrowIfEmptyString(sentenceName, nameof(sentenceName));
            Guard.ThrowIfNull(words, nameof(words));

            return sentenceName switch
            {
                "!done" => new ApiDoneSentence(words, _localizationService),
                "!trap" => new ApiTrapSentence(words, _localizationService),
                "!re" => new ApiReSentence(words, _localizationService),
                "!fatal" => new ApiFatalSentence(words, _localizationService),
                "" => throw new SentenceCreationFaultException(_localizationService.GetResponseTypeNotReceivedText(GetTextInternal(words, _localizationService))),
                _ => throw new SentenceCreationFaultException(_localizationService.GetUnknownResponseTypeText(sentenceName, GetTextInternal(words, _localizationService)))
            };
        }

        public IApiSentence CreateDoneSentence(IReadOnlyList<string> words) => new ApiDoneSentence(words, _localizationService);

        public IApiSentence CreateReSentence(IReadOnlyList<string> words) => new ApiReSentence(words, _localizationService);

        public IApiSentence CreateTrapSentence(IReadOnlyList<string> words) => new ApiTrapSentence(words, _localizationService);

        public IApiSentence CreateFatalSentence(IReadOnlyList<string> words) => new ApiFatalSentence(words, _localizationService);
    }
}