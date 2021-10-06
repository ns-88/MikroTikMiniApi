using System.Collections.Generic;
using MikroTikMiniApi.Interfaces.Sentences;
using MikroTikMiniApi.Interfaces.Services;

namespace MikroTikMiniApi.Sentences
{
    /// <summary>
    /// API sentence of the "Done" type.
    /// </summary>
    internal class ApiDoneSentence : ApiSentenceBase, IApiDoneSentence
    {
        internal ApiDoneSentence(IReadOnlyList<string> words, IApiSentenceLocalizationService localizationService)
            : base(words, localizationService)
        {
        }
    }
}