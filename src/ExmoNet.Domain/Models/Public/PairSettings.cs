using System.Text.Json.Serialization;

namespace ExmoNet.Domain.Models;

public record PairSettings
{
    [JsonPropertyName("min_quantity")] public decimal MinQuantity { get; set; }

    [JsonPropertyName("max_quantity")] public decimal MaxQuantity { get; set; }

    [JsonPropertyName("min_price")] public decimal MinPrice { get; set; }

    [JsonPropertyName("max_price")] public decimal MaxPrice { get; set; }

    [JsonPropertyName("max_amount")] public decimal MaxAmount { get; set; }

    [JsonPropertyName("min_amount")] public decimal MinAmount { get; set; }

    [JsonPropertyName("price_precision")] public int PricePrecision { get; set; }

    [JsonPropertyName("commission_taker_percent")]
    public decimal CommissionTakerPercent { get; set; }

    [JsonPropertyName("commission_maker_percent")]
    public decimal CommissionMakerPercent { get; set; }
}
