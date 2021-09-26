using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Models.Api
{
    public class Log : ModelBase, IModelFactory<Log>
    {
        public string Time { get; private set; }
        public string Topics { get; private set; }
        public string Message { get; private set; }

        Log IModelFactory<Log>.Create(IApiSentence sentence)
        {
            return new Log
            {
                Id = GetStringValue(".id", sentence),
                Time = GetStringValue("time", sentence),
                Topics = GetStringValue("topics", sentence),
                Message = GetStringValue("message", sentence)
            };
        }
    }
}