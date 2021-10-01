using System.Threading.Tasks;

namespace MikroTikMiniApi.Interfaces.Services
{
    internal interface IAuthenticationService
    {
        Task AuthenticationAsync(string name, string password);
        Task QuitAsync();
    }
}