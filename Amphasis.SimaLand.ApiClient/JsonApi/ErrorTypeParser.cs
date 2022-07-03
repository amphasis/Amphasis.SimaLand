using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Amphasis.SimaLand.Enums;

namespace Amphasis.SimaLand.JsonApi
{
	internal static class ErrorTypeParser
	{
		public static ErrorType? Parse(string errorTypeString)
		{
			if (errorTypeString == null) throw new ArgumentNullException(nameof(errorTypeString));

			if (_errorTypes.TryGetValue(errorTypeString, out var errorType)) return errorType;

			return null;
		}

		private static readonly IReadOnlyDictionary<string, ErrorType> _errorTypes;

		static ErrorTypeParser()
		{
			var enumValuesMembers =
				from memberInfo in typeof(ErrorType).GetFields(BindingFlags.Public | BindingFlags.Static)
				let enumMember = (ErrorType)Enum.Parse(typeof(ErrorType), memberInfo.Name)
				let attribute = memberInfo.GetCustomAttribute<EnumMemberAttribute>()
				where attribute != null
				select (attribute.Value, EnumMember: enumMember);

			_errorTypes = enumValuesMembers.ToDictionary(x => x.Value, x => x.EnumMember);
		}
	}
}