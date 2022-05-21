using System.Text.Json.Serialization;

namespace ExmoNet.Domain.Models.Public;

public record RequiredAmount
{
    public string FirstCurrency { get; set; }

    public string SecondCurrency { get; set; }

    [JsonPropertyName("quantity")] public decimal Quantity { get; set; }

    [JsonPropertyName("amount")] public decimal Amount { get; set; }

    [JsonPropertyName("avg_price")] public decimal AvgPrice { get; set; }
}
