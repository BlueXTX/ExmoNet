using System.Text.Json.Serialization;

namespace ExmoNet.Domain.Models;

public record Deal
{
    [JsonPropertyName("trade_id")] public long TradeId { get; set; }

    [JsonPropertyName("date")] public long Date { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;

    [JsonPropertyName("quantity")] public decimal Quantity { get; set; }

    [JsonPropertyName("price")] public decimal Price { get; set; }

    [JsonPropertyName("amount")] public decimal Amount { get; set; }
}
