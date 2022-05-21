using System.Text.Json;
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
                     ?? throw new InvalidOperationException();

        foreach (var deal in result)
        {
            deal.FirstCurrency = firstCurrency;
            deal.SecondCurrency = secondCurrency;
        }

        return result;
    }


    public Task<OrderBook> GetOrderBook(string firstCurrency, string secondCurrency, int limit)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TickerElement>> GetTicker()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PairSettings>> GetPairSettings()
    {
        throw new NotImplementedException();
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

    public Task<RequiredAmount> GetRequiredAmount(string firstCurrency, string secondCurrency, int quantity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Candle>> GetCandlesHistory(string firstCurrency,
        string secondCurrency,
        int resolution,
        DateTime from,
        DateTime to)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CryptoProvider>> GetCryptoProviders()
    {
        throw new NotImplementedException();
    }
}
