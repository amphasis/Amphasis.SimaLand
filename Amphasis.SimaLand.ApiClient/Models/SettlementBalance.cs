using System.Text.Json.Serialization;

namespace Amphasis.SimaLand.Models
{
	public class SettlementBalance
	{
		[JsonPropertyName("settlement_osm_id")]
		public int SettlementOsmId { get; set; }

		[JsonPropertyName("balance_text")]
		public string BalanceText { get; set; }
	}
}
