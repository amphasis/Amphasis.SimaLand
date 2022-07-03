using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Amphasis.SimaLand.Models
{
	public class ItemResponse
	{
		#region Identifiers
		
		[JsonPropertyName("sid")]
		public int Sid { get; set; }

		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("parent_item_id")]
		public int ParentItemId { get; set; }

		[JsonPropertyName("trademark_id")]
		public int TrademarkId { get; set; }

		[JsonPropertyName("unit_id")]
		public int UnitId { get; set; }

		[JsonPropertyName("nested_unit_id")]
		public int NestedUnitId { get; set; }

		[JsonPropertyName("category_id")]
		public int CategoryId { get; set; }

		[JsonPropertyName("country_id")]
		public int CountryId { get; set; }

		#endregion Identifiers

		#region Photos
		
		[JsonPropertyName("base_photo_url")]
		public string BasePhotoUrl { get; set; }

		[JsonPropertyName("agg_photos")]
		public IList<int> PhotoIdentifiers { get; set; }

		#endregion Photos

		#region Boolean Properties

		[JsonPropertyName("is_adult")]
		public bool IsAdult { get; set; }

		[JsonPropertyName("is_exclusive")]
		public bool IsExclusive { get; set; }

		[JsonPropertyName("is_markdown")]
		public bool IsMarkdown { get; set; }

		[JsonPropertyName("is_paid_delivery")]
		public bool IsPaidDelivery { get; set; }

		[JsonPropertyName("is_price_fixed")]
		public bool IsPriceFixed { get; set; }

		[JsonPropertyName("is_remote_store")]
		public bool IsRemoteStore { get; set; }

		#endregion Boolean Properties

		#region Balance

		[JsonPropertyName("balance")]
		public string Balance { get; set; }

		[JsonPropertyName("settlements_balance")]
		public IList<SettlementBalance> SettlementsBalance { get; set; }

		[JsonPropertyName("supply_period")]
		public int SupplyPeriod { get; set; }

		#endregion Balance

		#region Name Description
		
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("description")]
		public string DescriptionHtml { get; set; }

		[JsonPropertyName("slug")]
		public string UrlSlug { get; set; }

		[JsonPropertyName("barcodes")]
		public IList<string> Barcodes { get; set; }

		#endregion Name Description

		#region Prices

		[JsonPropertyName("price")]
		public decimal Price { get; set; }

		[JsonPropertyName("price_max")]
		public decimal PriceMax { get; set; }

		[JsonPropertyName("wholesale_price")]
		public decimal WholesalePrice { get; set; }

		#endregion Prices

		#region Quantity

		[JsonPropertyName("min_qty")]
		public int MinQty { get; set; }

		[JsonPropertyName("minimum_order_quantity")]
		public int MinimumOrderQuantity { get; set; }

		[JsonPropertyName("qty_multiplier")]
		public int QtyMultiplier { get; set; }

		#endregion Quantity

		#region Dimensions

		[JsonPropertyName("weight")]
		public decimal Weight { get; set; }

		[JsonPropertyName("width")]
		public decimal Width { get; set; }

		[JsonPropertyName("height")]
		public decimal Height { get; set; }

		[JsonPropertyName("depth")]
		public decimal Depth { get; set; }

		[JsonPropertyName("box_width")]
		public decimal BoxWidth { get; set; }

		[JsonPropertyName("box_height")]
		public decimal BoxHeight { get; set; }

		[JsonPropertyName("box_depth")]
		public decimal BoxDepth { get; set; }

		#endregion Dimensions
	}
}
