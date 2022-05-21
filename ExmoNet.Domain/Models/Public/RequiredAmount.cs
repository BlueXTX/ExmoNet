using System.Text.Json.Serialization;

namespace ExmoNet.Domain.Models;

public record RequiredAmount
{
    [JsonPropertyName("quantity")] public decimal Quantity { get; set; }

    [JsonPropertyName("amount")] public decimal Amount { get; set; }

    [JsonPropertyName("avg_price")] public decimal AvgPrice { get; set; }
}
