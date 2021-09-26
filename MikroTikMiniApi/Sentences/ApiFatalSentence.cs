using System.Collections.Generic;
using MikroTikMiniApi.Interfaces.Services;

namespace MikroTikMiniApi.Sentences
{
    internal class ApiFatalSentence : ApiSentenceBase
    {
        public ApiFatalSentence(IReadOnlyList<string> words, IApiSentenceLocalizationService localizationService)
            : base(words, localizationService)
        {
        }
    }
}