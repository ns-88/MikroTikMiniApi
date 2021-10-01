using System.Net;
using MikroTikMiniApi.Interfaces;
using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Models.Settings;
using MikroTikMiniApi.Interfaces.Networking;
using MikroTikMiniApi.Interfaces.Services;
using MikroTikMiniApi.Networking;
using MikroTikMiniApi.Services;

namespace MikroTikMiniApi.Factories
{
    public class MicrotikApiFactory : IApiFactory
    {
        private readonly ILocalizationService _localizationService;
        public IApiSentenceFactory ApiSentenceFactory { get; }

        public MicrotikApiFactory()
        {
            _localizationService = new LocalizationService();
            ApiSentenceFactory = new ApiSentenceFactory(_localizationService);
        }

        public IControlledConnection CreateConnection(IPEndPoint endPoint)
        {
            return new Connection(endPoint, _localizationService);
        }

        public IControlledConnection CreateConnection(IConnectionSettings settings)
        {
            return new Connection(settings, _localizationService);
        }

        public IRouterApi CreateRouterApi(IConnection connection)
        {
            return new MicrotikApi(connection, _localizationService, ApiSentenceFactory);
        }
    }
}