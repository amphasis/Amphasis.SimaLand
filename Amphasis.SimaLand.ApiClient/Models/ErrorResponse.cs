using System.Text.Json.Serialization;

namespace Amphasis.SimaLand.Models
{
    public class ErrorResponse
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
