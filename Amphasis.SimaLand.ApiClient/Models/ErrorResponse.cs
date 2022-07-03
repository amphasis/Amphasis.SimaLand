using System.Text.Json.Serialization;

namespace Amphasis.SimaLand.Models
{
	public class ErrorResponse
	{
		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}
