using System;
using System.Net.Http;
using System.Threading.Tasks;
using Amphasis.SimaLand.Models;

namespace Amphasis.SimaLand.JsonApi
{
	internal static class HttpResponseMessageExtensions
	{
		public static async Task<T> ReadJsonAsync<T>(this HttpResponseMessage httpResponseMessage)
		{
			if (httpResponseMessage == null) throw new ArgumentNullException(nameof(httpResponseMessage));

			await httpResponseMessage.EnsureSuccessAsync();
			var httpContent = httpResponseMessage.Content;
			httpContent.EnsureContentTypeIs(ContentTypes.ApplicationJson);
			var deserializedContent = await httpContent.ReadJsonAsync<T>();

			return deserializedContent;
		}

		public static async ValueTask EnsureSuccessAsync(this HttpResponseMessage httpResponseMessage)
		{
			if (httpResponseMessage == null) throw new ArgumentNullException(nameof(httpResponseMessage));

			if (httpResponseMessage.IsSuccessStatusCode) return;

			var httpContent = httpResponseMessage.Content;
			var errorType = TryGetErrorTypeString(httpContent);
			if (errorType == null) httpResponseMessage.EnsureSuccessStatusCode();

			var errorResponse = await httpContent.ReadJsonAsync<ErrorResponse>();

			throw SimaLandApiException.Create(
				errorResponse,
				ErrorTypeParser.Parse(errorType),
				httpResponseMessage.StatusCode);
		}

		private static string TryGetErrorTypeString(HttpContent httpContent)
		{
			var mediaType = httpContent.Headers.ContentType.MediaType;
			var match = ContentTypes.ApplicationErrorRegex.Match(mediaType);

			return match.Success
				? match.Captures[0].Value
				: null;
		}
	}
}
