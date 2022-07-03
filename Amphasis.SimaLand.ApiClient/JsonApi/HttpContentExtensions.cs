using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Amphasis.SimaLand.JsonApi
{
	internal static class HttpContentExtensions
	{
		public static HttpContent EnsureContentTypeIs(this HttpContent httpContent, string expectedContentType)
		{
			if (httpContent == null) throw new ArgumentNullException(nameof(httpContent));
			if (expectedContentType == null) throw new ArgumentNullException(nameof(expectedContentType));

			var actualMediaType = httpContent.Headers.ContentType.MediaType;
			var isExpectedContentType = actualMediaType.Equals(expectedContentType, StringComparison.OrdinalIgnoreCase);
			if (isExpectedContentType) return httpContent;
			var message = $"Expected {expectedContentType} content type, but {actualMediaType} specified in response";
			throw new HttpRequestException(message);
		}

		public static async Task<T> ReadJsonAsync<T>(this HttpContent httpContent)
		{
			using (var contentStream = await httpContent.ReadAsStreamAsync())
			{
				var deserializedObject = await JsonSerializer.DeserializeAsync<T>(contentStream);
				return deserializedObject;
			}
		}
	}
}
