using System.Text.Json.Serialization;

namespace ExmoNet.Domain.Models.Public;

public record OrderBook
{
    [JsonPropertyName("ask_quantity")] public decimal AskQuantity { get; set; }

    [JsonPropertyName("ask_amount")] public decimal AskAmount { get; set; }

    [JsonPropertyName("ask_top")] public decimal AskTop { get; set; }

    [JsonPropertyName("bid_quantity")] public decimal BidQuantity { get; set; }

    [JsonPropertyName("bid_amount")] public decimal BidAmount { get; set; }

    [JsonPropertyName("bid_top")] public decimal BidTop { get; set; }

    [JsonPropertyName("ask")] public IEnumerable<ShortDeal> Ask { get; set; }

    [JsonPropertyName("bid")] public IEnumerable<ShortDeal> Bid { get; set; }
}
