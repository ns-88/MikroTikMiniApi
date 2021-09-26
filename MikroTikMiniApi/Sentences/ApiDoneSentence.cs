using System.Collections.Generic;
using MikroTikMiniApi.Interfaces.Services;

namespace MikroTikMiniApi.Sentences
{
    internal class ApiDoneSentence : ApiSentenceBase
    {
        public ApiDoneSentence(IReadOnlyList<string> words, IApiSentenceLocalizationService localizationService)
            : base(words, localizationService)
        {
        }
    }
}