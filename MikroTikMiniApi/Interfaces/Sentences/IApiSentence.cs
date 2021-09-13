using System.Collections.Generic;

namespace MikroTikMiniApi.Interfaces.Sentences
{
    public interface IApiSentence
    {
        IReadOnlyList<string> Words { get; }

        bool TryGetWordValue(string word, out string value);

        string GetText();
    }
}