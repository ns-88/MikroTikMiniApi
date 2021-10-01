using System.Collections.Generic;
using MikroTikMiniApi.Interfaces.Services;

namespace MikroTikMiniApi.Sentences
{
    internal class ApiReSentence : ApiSentenceBase
    {
        public ApiReSentence(IReadOnlyList<string> words, IApiSentenceLocalizationService localizationService)
            : base(words, localizationService)
        {
        }
    }
}