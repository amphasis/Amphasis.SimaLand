using System;
using System.Net.Http;
using Amphasis.SimaLand.Models;

namespace Amphasis.SimaLand.JsonApi
{
    public class SimaLandApiException : HttpRequestException
    {
        public SimaLandApiException()
            : base()
        {
        }

        public SimaLandApiException(string message)
            : base(message)
        {
        }

        public SimaLandApiException(string message, Exception inner)
            : base(message, inner)
        {
        }

        private SimaLandApiException(ErrorResponse errorResponse, string message)
            : this(message)
        {
            ErrorResponse = errorResponse;
        }

        public ErrorResponse ErrorResponse { get; }

        public static SimaLandApiException FromError(ErrorResponse errorResponse)
        {
            if (errorResponse == null) throw new ArgumentNullException(nameof(errorResponse));

            string message = $"{errorResponse.Status} {errorResponse.Code} {errorResponse.Detail}";
            var exception = new SimaLandApiException(errorResponse, message);

            return exception;
        }
    }
}
