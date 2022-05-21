using System.Text.Json.Serialization;

namespace ExmoNet.Domain.Models;

public record Ticker
{
    [JsonPropertyName("buy_price")] public decimal BuyPrice { get; set; }

    [JsonPropertyName("sell_price")] public decimal SellPrice { get; set; }

    [JsonPropertyName("last_trade")] public decimal LastTrade { get; set; }

    [JsonPropertyName("high")] public decimal High { get; set; }

    [JsonPropertyName("low")] public decimal Low { get; set; }

    [JsonPropertyName("avg")] public decimal Avg { get; set; }

    [JsonPropertyName("vol")] public decimal Vol { get; set; }

    [JsonPropertyName("vol_curr")] public decimal VolCurr { get; set; }

    [JsonPropertyName("updated")] public long Updated { get; set; }
}
