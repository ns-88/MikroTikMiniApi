using System.Collections.Generic;
using MikroTikMiniApi.Interfaces.Services;

namespace MikroTikMiniApi.Sentences
{
    internal class ApiTrapSentence : ApiSentenceBase
    {
        public ApiTrapSentence(IReadOnlyList<string> words, IApiSentenceLocalizationService localizationService)
            : base(words, localizationService)
        {
        }
    }
}