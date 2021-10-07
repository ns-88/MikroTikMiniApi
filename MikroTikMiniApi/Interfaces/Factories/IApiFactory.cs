using System.Net;
using MikroTikMiniApi.Interfaces.Models.Settings;
using MikroTikMiniApi.Interfaces.Networking;

namespace MikroTikMiniApi.Interfaces.Factories
{
    /// <summary>
    /// API component factory.
    /// </summary>
    public interface IApiFactory
    {
        /// <summary>
        /// Returns a factory that creates API sentences.
        /// </summary>
        IApiSentenceFactory ApiSentenceFactory { get; }

        /// <summary>
        /// Creates only the connection object. Connection is not made.
        /// </summary>
        /// <param name="endPoint">The end point. Includes IP address and port.</param>
        /// <returns>Connection object.</returns>
        IControlledConnection CreateConnection(IPEndPoint endPoint);

        /// <summary>
        /// Creates only the connection object. Connection is not made.
        /// </summary>
        /// <param name="settings">Connection settings.</param>
        /// <returns>Connection object.</returns>
        IControlledConnection CreateConnection(IConnectionSettings settings);

        /// <summary>
        /// Creates an object containing all the methods for working with the API.
        /// </summary>
        /// <param name="connection">Connection object. The connection must be established.</param>
        /// <returns>API object.</returns>
        IRouterApi CreateRouterApi(IConnection connection);
    }
}