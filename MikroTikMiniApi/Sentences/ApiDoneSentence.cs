using System.Collections.Generic;

namespace MikroTikMiniApi.Sentences
{
    public class ApiDoneSentence : ApiSentenceBase
    {
        public ApiDoneSentence(IReadOnlyList<string> words)
            : base(words)
        {
        }
    }
}