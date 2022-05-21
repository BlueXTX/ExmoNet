using System.Globalization;
using System.Text.Json.Nodes;
using ExmoNet.Application.Exceptions;
using ExmoNet.Domain.Models.Public;
using RestSharp;
using RestSharp.Serializers.Json;

namespace ExmoNet.Application.Helpers;

public static class ExmoApiHelper
{
    internal static RestClient CreateDefaultClient()
    {
        return new RestClient(
                new RestClientOptions("https://api.exmo.com/v1.1"))
            .UseSystemTextJson();
    }

    internal static RestRequest AddContentTypeHeader(this RestRequest request)
    {
        return request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
    }

    internal static JsonObject ToJsonObject(this RestResponse response)
    {
        if (response.Content is null) throw new NoResponseContentException();

        return JsonNode.Parse(response.Content)?.AsObject() ?? throw new ResponseToJsonException();
    }

    internal static IEnumerable<ShortDeal> ConvertJsonArrayToShortDeals(JsonArray jsonArray)
    {
        if (jsonArray is null) throw new ArgumentNullException();

        return jsonArray.Select(node => new ShortDeal
        {
            Price = decimal.Parse(node?[0]?.ToString() ?? throw new ResponseToJsonException(),
                CultureInfo.InvariantCulture),
            Quantity = decimal.Parse(node[1]?.ToString() ?? throw new ResponseToJsonException(),
                CultureInfo.InvariantCulture),
            Amount = decimal.Parse(node[2]?.ToString() ?? throw new ResponseToJsonException(),
                CultureInfo.InvariantCulture)
        }).ToList();
    }
}
