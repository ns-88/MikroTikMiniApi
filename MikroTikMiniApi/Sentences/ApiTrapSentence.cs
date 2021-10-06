using System.Collections.Generic;
using MikroTikMiniApi.Interfaces.Sentences;
using MikroTikMiniApi.Interfaces.Services;

namespace MikroTikMiniApi.Sentences
{
    /// <summary>
    /// API sentence of the "Trap" type.
    /// </summary>
    internal class ApiTrapSentence : ApiSentenceBase, IApiTrapSentence
    {
        public ApiTrapSentence(IReadOnlyList<string> words, IApiSentenceLocalizationService localizationService)
            : base(words, localizationService)
        {
        }
    }
}