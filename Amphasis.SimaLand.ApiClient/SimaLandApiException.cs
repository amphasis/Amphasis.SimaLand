using System;
using System.Net;
using System.Net.Http;
using Amphasis.SimaLand.Enums;
using Amphasis.SimaLand.Models;

namespace Amphasis.SimaLand
{
	public class SimaLandApiException : HttpRequestException
	{
		public SimaLandApiException()
			: base("SimaLand API exception")
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

		public static SimaLandApiException Create(
			ErrorResponse response,
			ErrorType? errorType,
			HttpStatusCode? statusCode)
		{
			var exception = string.IsNullOrWhiteSpace(response?.Message)
				? new SimaLandApiException()
				: new SimaLandApiException($"SimaLand API exception: {response.Message}");

			exception.ErrorType = errorType;
			exception.StatusCode = statusCode;

			return exception;
		}

		public ErrorType? ErrorType { get; private set; }

		public HttpStatusCode? StatusCode { get; private set; }
	}
}
