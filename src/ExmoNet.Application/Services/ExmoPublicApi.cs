using System.Text.Json;
using System.Text.Json.Serialization;
using ExmoNet.Application.Helpers;
using ExmoNet.Domain.Models.Public;
using RestSharp;

namespace ExmoNet.Application.Services;

public class ExmoPublicApi
{
    public async Task<IEnumerable<Deal>> GetTrades(string firstCurrency, string secondCurrency)
    {
        var client = ExmoPublicApiHelper.CreateDefaultClient();
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

    public Task<IEnumerable<string>> GetCurrencies()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ExtendedCurrency>> GetExtendedCurrencies()
    {
        throw new NotImplementedException();
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
