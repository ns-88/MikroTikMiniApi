using System.Collections.Generic;

namespace MikroTikMiniApi.Interfaces.Sentences
{
    /// <summary>
    /// API sentence.
    /// </summary>
    public interface IApiSentence
    {
        /// <summary>
        /// Words that make up sentences.
        /// </summary>
        IReadOnlyList<string> Words { get; }

        /// <summary>
        /// Gets the value of the API word.
        /// </summary>
        /// <param name="word">API word.</param>
        /// <param name="value">Word value.</param>
        /// <returns>A sign that a word has meaning.</returns>
        bool TryGetWordValue(string word, out string value);

        /// <summary>
        /// Returns the words of a sentence as a single string.
        /// </summary>
        /// <returns>Text string.</returns>
        string GetText();
    }
}