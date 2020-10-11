using System;
using System.Net.Http;
using System.Threading.Tasks;
using Amphasis.SimaLand.Models;

namespace Amphasis.SimaLand.JsonApi
{
    internal static class HttpResponseMessageExtensions
    {
        private const string JsonContentType = "application/vnd.simaland.attribute+json";
        private const string ErrorContentType = "application/vnd.goa.error";

        public static async Task<T> ReadJsonAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage == null) throw new ArgumentNullException(nameof(httpResponseMessage));

            await httpResponseMessage.EnsureSuccessAsync();
            var httpContent = httpResponseMessage.Content;
            httpContent.EnsureContentTypeIs(JsonContentType);
            var deserializedContent = await httpContent.ReadJsonAsync<T>();

            return deserializedContent;
        }

        public static async ValueTask EnsureSuccessAsync(this HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage == null) throw new ArgumentNullException(nameof(httpResponseMessage));

            if (httpResponseMessage.IsSuccessStatusCode) return;

            var httpContent = httpResponseMessage.Content;
            if (!httpContent.ContentTypeIs(ErrorContentType)) httpResponseMessage.EnsureSuccessStatusCode();
            var errorResponse = await httpContent.ReadJsonAsync<ErrorResponse>();

            throw SimaLandApiException.FromError(errorResponse);
        }
    }
}
