using System.Collections.Generic;
using MikroTikMiniApi.Interfaces.Sentences;
using MikroTikMiniApi.Interfaces.Services;

namespace MikroTikMiniApi.Sentences
{
    /// <summary>
    /// API sentence of the "Re" type.
    /// </summary>
    internal class ApiReSentence : ApiSentenceBase, IApiReSentence
    {
        public ApiReSentence(IReadOnlyList<string> words, IApiSentenceLocalizationService localizationService)
            : base(words, localizationService)
        {
        }
    }
}