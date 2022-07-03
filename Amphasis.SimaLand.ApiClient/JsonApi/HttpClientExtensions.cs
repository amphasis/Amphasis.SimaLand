using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Amphasis.SimaLand.JsonApi
{
	internal static class HttpClientExtensions
	{
		private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
		{
			IgnoreNullValues = true
		};

		public static async Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient httpClient, Uri uri, T requestContent)
		{
			using (var httpContent = new JsonContent(requestContent, JsonSerializerOptions))
			{
				var httpResponseMessage = await httpClient.PostAsync(uri, httpContent);
				return httpResponseMessage;
			}
		}
	}
}