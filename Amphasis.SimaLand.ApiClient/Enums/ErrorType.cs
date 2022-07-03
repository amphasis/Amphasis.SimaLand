using System.Runtime.Serialization;

namespace Amphasis.SimaLand.Enums
{
	public enum ErrorType
	{
		[EnumMember(Value = "bad_request")]
		BadRequest = 400,

		[EnumMember(Value = "unauthorized")]
		Unauthorized = 401,

		[EnumMember(Value = "internal_server_error")]
		InternalServerError = 500,
	}
}