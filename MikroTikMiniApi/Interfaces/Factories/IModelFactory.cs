using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Interfaces.Factories
{
    /// <summary>
    /// Data model factory.
    /// </summary>
    /// <typeparam name="T">Model type.</typeparam>
    public interface IModelFactory<out T> where T : class
    {
        /// <summary>
        /// Creates a data model.
        /// </summary>
        /// <param name="sentence">The API sentence from which the model is created.</param>
        /// <returns>Data model.</returns>
        T Create(IApiSentence sentence);
    }
}