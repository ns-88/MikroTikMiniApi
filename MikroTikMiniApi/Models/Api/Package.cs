using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Models.Api
{
    public class Package : ModelBase, IModelFactory<Package>
    {
        public string? Name { get; private set; }
        public string? Version { get; private set; }
        public string? BuildTime { get; private set; }
        public string? Scheduled { get; private set; }
        public bool? IsDisabled { get; private set; }

        Package IModelFactory<Package>.Create(IApiSentence sentence)
        {
            return new Package
            {
                Id = GetStringValueOrDefault(".id", sentence),
                Name = GetStringValueOrDefault("name", sentence),
                Version = GetStringValueOrDefault("version", sentence),
                BuildTime = GetStringValueOrDefault("build-time", sentence),
                Scheduled = GetStringValueOrDefault("scheduled", sentence),
                IsDisabled = GetBoolValueOrDefault("disabled", sentence)
            };
        }
    }
}