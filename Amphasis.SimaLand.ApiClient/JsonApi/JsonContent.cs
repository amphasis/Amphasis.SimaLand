using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Amphasis.SimaLand.JsonApi
{
    internal class JsonContent : ByteArrayContent
    {
        public JsonContent(object contentObject, JsonSerializerOptions jsonSerializerOptions = null)
            : base(SerializeToByteArray(contentObject, jsonSerializerOptions))
        {
            Headers.ContentType = new MediaTypeHeaderValue("application/json") {CharSet = Encoding.UTF8.WebName};
        }

        private static byte[] SerializeToByteArray(object contentObject, JsonSerializerOptions jsonSerializerOptions)
        {
            var objectType = contentObject?.GetType() ?? typeof(object);
            return JsonSerializer.SerializeToUtf8Bytes(contentObject, objectType, jsonSerializerOptions);
        }
    }
}