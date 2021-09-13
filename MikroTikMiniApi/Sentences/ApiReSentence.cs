using System.Collections.Generic;

namespace MikroTikMiniApi.Sentences
{
    public class ApiReSentence : ApiSentenceBase
    {
        public ApiReSentence(IReadOnlyList<string> words)
            : base(words)
        {
        }
    }
}