using System;
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

        [SetUp]
        public void Setup()
        {
            var httpClient = new HttpClient();
            _client = new SimaLandApiClient(httpClient);
            
            _email = Environment.GetEnvironmentVariable(EmailEnvironmentVariableName)
                ?? throw EnvironmentVariableException(EmailEnvironmentVariableName);

            _password = Environment.GetEnvironmentVariable(PasswordEnvironmentVariableName)
                ?? throw EnvironmentVariableException(PasswordEnvironmentVariableName);

            static Exception EnvironmentVariableException(string variableName)
            {
                string message = $"{variableName} environment variable not set";
                return new InvalidOperationException(message);
            }
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
