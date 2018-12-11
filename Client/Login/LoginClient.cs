using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Login
{
    class LoginClient
    {
        private readonly LoginService _loginService;

        public LoginClient()
        {
            _loginService = Program.DiContainer.ServiceProvider.GetRequiredService<LoginService>();
        }

        public async Task<string> GetContainerSasUri(string username)
        {
            var html = await _loginService.GetContainerSasUri(username);
            return html;
        }
    }
}
