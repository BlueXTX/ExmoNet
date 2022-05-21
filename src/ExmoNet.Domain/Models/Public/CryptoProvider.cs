using System.Text.Json.Serialization;

namespace ExmoNet.Domain.Models;

public record CryptoProvider
{
    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("currency_name")] public string CurrencyName { get; set; }

    [JsonPropertyName("min")] public decimal Min { get; set; }

    [JsonPropertyName("max")] public decimal Max { get; set; }

    [JsonPropertyName("enabled")] public bool Enabled { get; set; }

    [JsonPropertyName("comment")] public string Comment { get; set; }

    [JsonPropertyName("commission_desc")] public string CommissionDesc { get; set; }

    [JsonPropertyName("currency_confirmations")]
    public int CurrencyConfirmations { get; set; }
}
