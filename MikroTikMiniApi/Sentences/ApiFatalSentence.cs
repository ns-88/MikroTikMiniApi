using System.Collections.Generic;
using MikroTikMiniApi.Interfaces.Sentences;
using MikroTikMiniApi.Interfaces.Services;

namespace MikroTikMiniApi.Sentences
{
    /// <summary>
    /// API sentence of the "Fatal" type.
    /// </summary>
    internal class ApiFatalSentence : ApiSentenceBase, IApiFatalSentence
    {
        public ApiFatalSentence(IReadOnlyList<string> words, IApiSentenceLocalizationService localizationService)
            : base(words, localizationService)
        {
        }
    }
}