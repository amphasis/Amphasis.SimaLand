using System.Text.Json.Serialization;

namespace Amphasis.SimaLand.Models
{
    public class SignInRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("regulation")]
        public bool Regulation { get; set; }
    }
}
