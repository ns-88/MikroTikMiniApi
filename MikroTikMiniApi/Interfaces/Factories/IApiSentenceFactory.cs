using System.Collections.Generic;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Interfaces.Factories
{
    /// <summary>
    /// A factory that creates API sentences.
    /// </summary>
    public interface IApiSentenceFactory
    {
        /// <summary>
        /// Creates an API sentence.
        /// </summary>
        /// <param name="sentenceName">Sentence name.</param>
        /// <param name="words">Words that make up a sentence.</param>
        /// <returns>API sentence.</returns>
        internal IApiSentence Create(string sentenceName, IReadOnlyList<string> words);

        /// <summary>
        /// Creates an API sentence of the "Done" type.
        /// </summary>
        /// <param name="words">Words that make up a sentence.</param>
        /// <returns>API sentence.</returns>
        IApiDoneSentence CreateDoneSentence(IReadOnlyList<string> words);

        /// <summary>
        /// Creates an API sentence of the "Re" type.
        /// </summary>
        /// <param name="words">Words that make up a sentence.</param>
        /// <returns>API sentence.</returns>
        IApiReSentence CreateReSentence(IReadOnlyList<string> words);

        /// <summary>
        /// Creates an API sentence of the "Trap" type.
        /// </summary>
        /// <param name="words">Words that make up a sentence.</param>
        /// <returns>API sentence.</returns>
        IApiTrapSentence CreateTrapSentence(IReadOnlyList<string> words);

        /// <summary>
        /// Creates an API sentence of the "Fatal" type.
        /// </summary>
        /// <param name="words">Words that make up a sentence.</param>
        /// <returns>API sentence.</returns>
        IApiFatalSentence CreateFatalSentence(IReadOnlyList<string> words);
    }
}