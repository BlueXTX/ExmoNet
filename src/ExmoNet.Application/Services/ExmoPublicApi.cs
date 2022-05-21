using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ExmoNet.Application.Exceptions;
using ExmoNet.Application.Helpers;
using ExmoNet.Domain.Models.Public;
using RestSharp;

namespace ExmoNet.Application.Services;

public class ExmoPublicApi
{
    public async Task<IEnumerable<Deal>> GetTrades(string firstCurrency, string secondCurrency)
    {
        var client = ExmoApiHelper.CreateDefaultClient();
        string pair = $"{firstCurrency}_{secondCurrency}";
        var request = new RestRequest("trades", Method.Post)
            .AddContentTypeHeader()
            .AddParameter("pair", pair);
        var jsonObject = (await client.ExecuteAsync(request)).ToJsonObject();
        var jsonArray = jsonObject[pair]?.AsArray();
        var result = jsonArray.Deserialize<Deal[]>(new JsonSerializerOptions
                         { NumberHandling = JsonNumberHandling.AllowReadingFromString })
                     ?? throw new ResponseToJsonException();

        foreach (var deal in result)
        {
            deal.FirstCurrency = firstCurrency;
            deal.SecondCurrency = secondCurrency;
        }

        return result;
    }


    public async Task<OrderBook> GetOrderBook(string firstCurrency, string secondCurrency, int limit)
    {
        var client = ExmoApiHelper.CreateDefaultClient();
        string pair = $"{firstCurrency}_{secondCurrency}";
        var request = new RestRequest("order_book", Method.Post)
            .AddContentTypeHeader()
            .AddParameter("pair", pair)
            .AddParameter("limit", limit);
        var jsonObject = (await client.ExecuteAsync(request)).ToJsonObject();
        var orderBookJsonObject = jsonObject[pair];
        var result = orderBookJsonObject.Deserialize<OrderBook>(new JsonSerializerOptions
            { NumberHandling = JsonNumberHandling.AllowReadingFromString });
        if (result is null) throw new ResponseToJsonException();
        result.Ask =
            ConvertArrayToShortDeals(orderBookJsonObject?["ask"]?.AsArray() ?? throw new ResponseToJsonException());
        result.Bid =
            ConvertArrayToShortDeals(orderBookJsonObject["bid"]?.AsArray() ?? throw new ResponseToJsonException());
        result.FirstCurrency = firstCurrency;
        result.SecondCurrency = secondCurrency;
        return result;
    }

    private IEnumerable<ShortDeal> ConvertArrayToShortDeals(JsonArray jsonArray)
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

    public async Task<IEnumerable<TickerElement>> GetTicker()
    {
        var client = ExmoApiHelper.CreateDefaultClient();
        var request = new RestRequest("ticker")
            .AddContentTypeHeader();
        var jsonObject = (await client.ExecuteAsync(request)).ToJsonObject();
        var result = new List<TickerElement>();

        foreach (var keyValuePair in jsonObject)
        {
            var obj = keyValuePair.Value.Deserialize<TickerElement>(new JsonSerializerOptions
                          { NumberHandling = JsonNumberHandling.AllowReadingFromString })
                      ?? throw new ResponseToJsonException();
            string[] pair = keyValuePair.Key.Split('_');
            obj.FirstCurrency = pair[0];
            obj.SecondCurrency = pair[1];
            result.Add(obj);
        }

        return result;
    }

    public async Task<IEnumerable<PairSettings>> GetPairsSettings()
    {
        var client = ExmoApiHelper.CreateDefaultClient();
        var request = new RestRequest("pair_settings")
            .AddContentTypeHeader();
        var jsonObject = (await client.ExecuteAsync(request)).ToJsonObject();
        var result = new List<PairSettings>();

        foreach (var keyValuePair in jsonObject)
        {
            var obj = keyValuePair.Value.Deserialize<PairSettings>(new JsonSerializerOptions
                          { NumberHandling = JsonNumberHandling.AllowReadingFromString })
                      ?? throw new ResponseToJsonException();
            string[] pair = keyValuePair.Key.Split('_');
            obj.FirstCurrency = pair[0];
            obj.SecondCurrency = pair[1];
            result.Add(obj);
        }

        return result;
    }

    public async Task<IEnumerable<string>> GetCurrencies()
    {
        var client = ExmoApiHelper.CreateDefaultClient();
        var request = new RestRequest("currency")
            .AddContentTypeHeader();
        var response = await client.ExecuteAsync<IEnumerable<string>>(request);
        return response.Data ?? throw new ResponseToJsonException();
    }

    public async Task<IEnumerable<ExtendedCurrency>> GetExtendedCurrencies()
    {
        var client = ExmoApiHelper.CreateDefaultClient();
        var request = new RestRequest("currency/list/extended")
            .AddContentTypeHeader();
        var response = await client.ExecuteAsync<IEnumerable<ExtendedCurrency>>(request);
        return response.Data ?? throw new ResponseToJsonException();
    }

    public async Task<RequiredAmount> GetRequiredAmount(string firstCurrency, string secondCurrency, decimal quantity)
    {
        var client = ExmoApiHelper.CreateDefaultClient();
        string pair = $"{firstCurrency}_{secondCurrency}";
        var request = new RestRequest("required_amount", Method.Post)
            .AddContentTypeHeader()
            .AddParameter("pair", pair)
            .AddParameter("quantity", quantity);
        var response = await client.ExecuteAsync<RequiredAmount>(request);

        if (response.Data is null) throw new ResponseToJsonException();
        response.Data.FirstCurrency = firstCurrency;
        response.Data.SecondCurrency = secondCurrency;
        return response.Data;
    }

    public async Task<IEnumerable<Candle>> GetCandlesHistory(string firstCurrency,
        string secondCurrency,
        int resolution,
        DateTimeOffset from,
        DateTimeOffset to)
    {
        var client = ExmoApiHelper.CreateDefaultClient();
        string symbol = $"{firstCurrency}_{secondCurrency}";
        var request = new RestRequest("candles_history")
            .AddQueryParameter("symbol", symbol)
            .AddQueryParameter("resolution", resolution)
            .AddQueryParameter("from", from.ToUnixTimeSeconds())
            .AddQueryParameter("to", to.ToUnixTimeSeconds());

        var jsonObject = (await client.ExecuteAsync(request)).ToJsonObject();
        var jsonArray = jsonObject["candles"]?.AsArray();
        var result = jsonArray.Deserialize<Candle[]>(new JsonSerializerOptions
                         { NumberHandling = JsonNumberHandling.AllowReadingFromString })
                     ?? throw new ResponseToJsonException();

        foreach (var candle in result)
        {
            candle.FirstCurrency = firstCurrency;
            candle.SecondCurrency = secondCurrency;
        }

        return result;
    }

    public async Task<IEnumerable<IEnumerable<CryptoProvider>>> GetCryptoProviders()
    {
        var client = ExmoApiHelper.CreateDefaultClient();
        var request = new RestRequest("payments/providers/crypto/list")
            .AddContentTypeHeader();
        var jsonObject = (await client.ExecuteAsync(request)).ToJsonObject();

        return jsonObject.Select(keyValuePair =>
                keyValuePair.Value?.AsArray().Deserialize<IEnumerable<CryptoProvider>>(new JsonSerializerOptions
                    { NumberHandling = JsonNumberHandling.AllowReadingFromString }) ??
                throw new ResponseToJsonException())
            .ToList();
    }
}
