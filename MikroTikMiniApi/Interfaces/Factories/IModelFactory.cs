using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Interfaces.Factories
{
    public interface IModelFactory<out T> where T : class
    {
        T Create(IApiSentence sentence);
    }
}