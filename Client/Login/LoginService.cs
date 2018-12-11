using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Login
{
    class LoginService
    {
        private HttpClient _httpClient { get; }

        public LoginService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<string> GetContainerSasUri(string username)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, username);
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
