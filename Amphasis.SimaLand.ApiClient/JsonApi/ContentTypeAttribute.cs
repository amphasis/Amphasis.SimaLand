using System;

namespace Amphasis.SimaLand.JsonApi
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ContentTypeAttribute : Attribute
    {
        public string ContentType { get; }

        public ContentTypeAttribute(string contentType)
        {
            ContentType = contentType;
        }
    }
}