using System.Text.Json.Serialization;

namespace ExmoNet.Domain.Models;

public record Candle
{
    [JsonPropertyName("t")] public long Time { get; set; }

    [JsonPropertyName("o")] public decimal Open { get; set; }

    [JsonPropertyName("c")] public decimal Close { get; set; }

    [JsonPropertyName("h")] public decimal High { get; set; }

    [JsonPropertyName("l")] public decimal Low { get; set; }

    [JsonPropertyName("v")] public decimal Volume { get; set; }
}
