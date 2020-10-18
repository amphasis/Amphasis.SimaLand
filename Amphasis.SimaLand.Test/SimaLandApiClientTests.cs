using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Amphasis.SimaLand.Test
{
    public class SimaLandApiClientTests
    {
        private const string EmailEnvironmentVariableName = "SIMALAND_EMAIL";
        private const string PasswordEnvironmentVariableName = "SIMALAND_PASSWORD";

        private SimaLandApiClient _client;
        private string _email;
        private string _password;

        [OneTimeSetUp]
        public async Task SetupAsync()
        {
            var httpClient = new HttpClient();
            _client = new SimaLandApiClient(httpClient);
            
            _email = Environment.GetEnvironmentVariable(EmailEnvironmentVariableName)
                ?? throw EnvironmentVariableException(EmailEnvironmentVariableName);

            _password = Environment.GetEnvironmentVariable(PasswordEnvironmentVariableName)
                ?? throw EnvironmentVariableException(PasswordEnvironmentVariableName);

            string token = await _client.GetAccessTokenAsync(_email, _password);
            _client.SetAccessToken(token);

            static Exception EnvironmentVariableException(string variableName)
            {
                string message = $"{variableName} environment variable not set";
                return new InvalidOperationException(message);
            }
        }

        [Test]
        public async Task GetAccessTokenAsync_ReturnsValidJwtToken_IfValidCredentialsProvided()
        {
            string token = await _client.GetAccessTokenAsync(_email, _password);
            var jwtSecurityToken = new JwtSecurityToken(token);
            Assert.Greater(jwtSecurityToken.ValidTo, DateTime.Now, "jwtSecurityToken.ValidTo > DateTime.Now");
            _client.SetAccessToken(token);
        }

        [TestCase(178000, ExpectedResult = 280875)]
        [TestCase(400000, ExpectedResult = 574038)]
        [TestCase(2000000, ExpectedResult = 2379312)]
        public async Task<int> GetItemAsync_ReturnsItem_ByIdentifier(int itemId)
        {
            var item = await _client.GetItemAsync(itemId);
            return item.Sid;
        }

        [Test]
        public async Task GetItemsAsync_ReturnsItemCollection()
        {
            const int pageId = 1;
            var itemList = await _client.GetItemsAsync(pageId);
            Assert.AreEqual(100, itemList.Count, "itemList.Count == 100");
        }
    }
}
