using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Amphasis.SimaLand.Test
{
    public class SimaLandApiClientTests
    {
        private SimaLandApiClient _client;
        private string _email;
        private string _password;

        [SetUp]
        public void Setup()
        {
            var httpClient = new HttpClient();
            _client = new SimaLandApiClient(httpClient);
            _email = Environment.GetEnvironmentVariable("SIMALAND_EMAIL");
            _password = Environment.GetEnvironmentVariable("SIMALAND_PASSWORD");
        }

        [Test]
        public async Task GetAccessTokenAsync_ReturnsBearerToken_IfValidCredentialsPassed()
        {
            string token = await _client.GetAccessTokenAsync(_email, _password);
            string tokenType = token.Substring(0, token.IndexOf(' '));
            Assert.AreEqual("Bearer", tokenType, "tokenType == \"Bearer\"");
        }
    }
}
