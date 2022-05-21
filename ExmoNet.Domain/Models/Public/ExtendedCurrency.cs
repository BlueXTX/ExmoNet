using System.Text.Json.Serialization;

namespace ExmoNet.Domain.Models;

public record ExtendedCurrency
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; }
}
