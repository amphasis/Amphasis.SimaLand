using System.Text.RegularExpressions;

namespace Amphasis.SimaLand.JsonApi
{
	internal static class ContentTypes
	{
		public const string ApplicationJson = "application/json";

		public static readonly Regex ApplicationErrorRegex =
			new Regex(@"/^application\/vnd\.simaland\.error\.([a-z_]*)\+json$/gm");
	}
}