using System.Collections.Generic;

namespace MikroTikMiniApi.Sentences
{
    public class ApiTrapSentence : ApiSentenceBase
    {
        public ApiTrapSentence(IReadOnlyList<string> words)
            : base(words)
        {
        }
    }
}