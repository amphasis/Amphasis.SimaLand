using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Amphasis.SimaLand.JsonApi;
using Amphasis.SimaLand.Models;

namespace Amphasis.SimaLand
{
    /// <summary>
    /// Клиент https://www.sima-land.ru/api/v5
    /// </summary>
    public class SimaLandApiClient
    {
        private const string BaseUriString = "https://www.sima-land.ru/api/v5/";

        private readonly HttpClient _httpClient;

        public SimaLandApiClient() : this(new HttpClient())
        {
        }

        public SimaLandApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUriString, UriKind.Absolute);
        }

        /// <summary>
        /// Маркер авторизации доступа к API
        /// </summary>
        public string AccessToken { get; set; }

        public async Task<string> GetAccessTokenAsync(string email, string password)
        {
            const string signinUriString = "signin";
            var uri = new Uri(signinUriString, UriKind.Relative);

            var request = new SignInRequest
            {
                Email = email,
                Password = password,
                Regulation = true
            };

            using (var httpResponseMessage = await _httpClient.PostJsonAsync(uri, request))
            {
                await httpResponseMessage.EnsureSuccessAsync();
                var headers = httpResponseMessage.Headers;
                const string authorizationHeaderName = "Authorization";
                var authorizationHeaderValuesEnumerable = headers.GetValues(authorizationHeaderName);
                string token = authorizationHeaderValuesEnumerable.FirstOrDefault();

                return token;
            }
        }
    }
}