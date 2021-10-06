using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Models.Api
{
    public class Service : ModelBase, IModelFactory<Service>
    { 
        public string? Name { get; private set; }
        public int? Port { get; private set; }
        public string? Address { get; private set; }
        public bool? IsInvalid { get; private set; }
        public bool? IsDisabled { get; private set; }

        Service IModelFactory<Service>.Create(IApiSentence sentence)
        {
            return new Service
            {
                Id = GetStringValueOrDefault(".id", sentence),
                Name = GetStringValueOrDefault("name", sentence),
                Port = GetIntValueOrDefault("port", sentence),
                Address = GetStringValueOrDefault("address", sentence),
                IsInvalid = GetBoolValueOrDefault("invalid", sentence),
                IsDisabled = GetBoolValueOrDefault("disabled", sentence)
            };
        }
    }
}