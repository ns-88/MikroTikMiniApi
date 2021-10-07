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
    ///<inheritdoc cref="IApiFactory"/>
    public class MicrotikApiFactory : IApiFactory
    {
        private readonly ILocalizationService _localizationService;

        ///<inheritdoc/>
        public IApiSentenceFactory ApiSentenceFactory { get; }

        public MicrotikApiFactory()
        {
            _localizationService = new LocalizationService();
            ApiSentenceFactory = new ApiSentenceFactory(_localizationService);
        }

        ///<inheritdoc/>
        public IControlledConnection CreateConnection(IPEndPoint endPoint)
        {
            return new Connection(endPoint, _localizationService);
        }

        ///<inheritdoc/>
        public IControlledConnection CreateConnection(IConnectionSettings settings)
        {
            return new Connection(settings, _localizationService);
        }

        ///<inheritdoc/>
        public IRouterApi CreateRouterApi(IConnection connection)
        {
            return new MicrotikApi(connection, _localizationService, ApiSentenceFactory);
        }
    }
}