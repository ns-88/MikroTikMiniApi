using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Models
{
    public class Package : ModelBase, IModelFactory<Package>
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Version { get; private set; }
        public string BuildTime { get; private set; }
        public string Scheduled { get; private set; }
        public bool? IsDisabled { get; private set; }

        Package IModelFactory<Package>.Create(IApiSentence sentence)
        {
            return new Package
            {
                Id = GetStringValue(".id", sentence),
                Name = GetStringValue("name", sentence),
                Version = GetStringValue("version", sentence),
                BuildTime = GetStringValue("build-time", sentence),
                Scheduled = GetStringValue("scheduled", sentence),
                IsDisabled = GetBoolValue("disabled", sentence)
            };
        }
    }
}