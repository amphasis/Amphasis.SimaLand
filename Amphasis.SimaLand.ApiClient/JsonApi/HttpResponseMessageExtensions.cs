using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Amphasis.SimaLand.Models;

namespace Amphasis.SimaLand.JsonApi
{
    internal static class HttpResponseMessageExtensions
    {
        private const string ApplicationJsonContentType = "application/json";

        private static readonly string ErrorContentType = typeof(ErrorResponse)
            .GetCustomAttribute<ContentTypeAttribute>()
            ?.ContentType ?? ApplicationJsonContentType;

        public static async Task<T> ReadJsonAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage == null) throw new ArgumentNullException(nameof(httpResponseMessage));

            await httpResponseMessage.EnsureSuccessAsync();
            var httpContent = httpResponseMessage.Content;
            string contentType = GetContentType<T>();
            httpContent.EnsureContentTypeIs(contentType);
            var deserializedContent = await httpContent.ReadJsonAsync<T>();

            return deserializedContent;
        }

        private static string GetContentType<T>()
        {
            var type = typeof(T);

            var contentTypeAttribute = 
                type.GetCustomAttribute<ContentTypeAttribute>() ??
                type.GenericTypeArguments.FirstOrDefault()?.GetCustomAttribute<ContentTypeAttribute>();

            string contentType = contentTypeAttribute?.ContentType ?? ApplicationJsonContentType;

            return contentType;
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
