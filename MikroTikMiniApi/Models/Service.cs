using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Models
{
    public class Service : ModelBase, IModelFactory<Service>
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public int Port { get; private set; }
        public string Address { get; private set; }
        public bool? IsInvalid { get; private set; }
        public bool? IsDisabled { get; private set; }

        Service IModelFactory<Service>.Create(IApiSentence sentence)
        {
            return new Service
            {
                Id = GetStringValue(".id", sentence),
                Name = GetStringValue("name", sentence),
                Port = GetIntValue("port", sentence),
                Address = GetStringValue("address", sentence),
                IsInvalid = GetBoolValue("invalid", sentence),
                IsDisabled = GetBoolValue("disabled", sentence)
            };
        }
    }
}