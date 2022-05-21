using ExmoNet.Domain.Models.Public;

namespace ExmoNet.Application.Services;

public class ExmoPublicApi
{
    public Task<IEnumerable<Deal>> GetTrades(string firstPair, string secondPair)
    {
        throw new NotImplementedException();
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
