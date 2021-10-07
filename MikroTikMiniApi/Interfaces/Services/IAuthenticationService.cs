using System.Threading.Tasks;

namespace MikroTikMiniApi.Interfaces.Services
{
    /// <summary>
    /// Service for user authentication and logout.
    /// </summary>
    internal interface IAuthenticationService
    {
        /// <summary>
        /// Authentication on the router. Must be performed before calling any other methods.
        /// </summary>
        /// <param name="name">User name.</param>
        /// <param name="password">User password.</param>
        /// <returns>A task to wait for.</returns>
        Task AuthenticationAsync(string name, string password);

        /// <summary>
        /// Log out of the system.
        /// </summary>
        /// <returns>A task to wait for.</returns>
        Task QuitAsync();
    }
}