using System.Collections.Generic;

namespace MikroTikMiniApi.Sentences
{
    public class ApiFatalSentence : ApiSentenceBase
    {
        public ApiFatalSentence(IReadOnlyList<string> words)
            : base(words)
        {
        }
    }
}