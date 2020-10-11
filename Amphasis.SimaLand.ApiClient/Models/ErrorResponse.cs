using System.Text.Json.Serialization;
using Amphasis.SimaLand.JsonApi;

namespace Amphasis.SimaLand.Models
{
    [ContentType("application/vnd.goa.error")]
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
