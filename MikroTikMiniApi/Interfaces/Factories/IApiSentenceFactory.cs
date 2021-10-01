using System.Collections.Generic;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Interfaces.Factories
{
    public interface IApiSentenceFactory
    {
        internal IApiSentence Create(string sentenceName, IReadOnlyList<string> words);

        IApiSentence CreateDoneSentence(IReadOnlyList<string> words);

        IApiSentence CreateReSentence(IReadOnlyList<string> words);

        IApiSentence CreateTrapSentence(IReadOnlyList<string> words);

        IApiSentence CreateFatalSentence(IReadOnlyList<string> words);
    }
}