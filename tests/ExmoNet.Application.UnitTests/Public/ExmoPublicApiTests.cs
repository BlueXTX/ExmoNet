using ExmoNet.Application.Services;
using FluentAssertions;

namespace ExmoNet.Application.UnitTests.Public;

public class ExmoPublicApiTests
{
    private const string FirstCurrency = "BTC";
    private const string SecondCurrency = "USD";

    [Fact]
    private async Task GetTradesTest()
    {
        var api = new ExmoPublicApi();
        var trades = await api.GetTrades(FirstCurrency, SecondCurrency);
        trades.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    private async Task GetOrderBookTest()
    {
        var api = new ExmoPublicApi();
        const int limit = 10;
        var orderBook = await api.GetOrderBook(FirstCurrency, SecondCurrency, limit);
        orderBook.Ask.Should().HaveCount(10);
    }

    [Fact]
    private async Task GetTickerTest()
    {
        var api = new ExmoPublicApi();
        var tickerElements = await api.GetTicker();
        var element = tickerElements.FirstOrDefault(x =>
            x.FirstCurrency == FirstCurrency && x.SecondCurrency == SecondCurrency);
        element.Should().NotBeNull();
    }

    [Fact]
    private async Task GetPairsSettingsTest()
    {
        var api = new ExmoPublicApi();
        var pairsSettings = await api.GetPairSettings();
        var settings = pairsSettings.FirstOrDefault(x =>
            x.FirstCurrency == FirstCurrency && x.SecondCurrency == SecondCurrency);
        settings.Should().NotBeNull();
    }

    [Fact]
    private async Task GetCurrenciesTest()
    {
        var api = new ExmoPublicApi();
        var currencies = await api.GetCurrencies();
        currencies.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    private async Task GetExtendedCurrenciesTest()
    {
        var api = new ExmoPublicApi();
        var currencies = await api.GetExtendedCurrencies();
        currencies.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    private async Task GetRequiredAmountTest()
    {
        var api = new ExmoPublicApi();
        const int quantity = 1;
        var requiredAmount = await api.GetRequiredAmount(FirstCurrency, SecondCurrency, quantity);
        requiredAmount.Quantity.Should().Be(quantity);
    }

    [Fact]
    private async Task GetCandlesHistoryTest()
    {
        var api = new ExmoPublicApi();
        const int resolution = 30;
        var from = DateTime.Now.Subtract(new TimeSpan(1, 0, 0));
        var to = DateTime.Now;
        var candles = await api.GetCandlesHistory(FirstCurrency, SecondCurrency, resolution, from, to);
        candles.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    private async Task GetCryptoProvidersTest()
    {
        var api = new ExmoPublicApi();
        var providers = await api.GetCryptoProviders();
        providers.Should().HaveCountGreaterThan(0);
    }
}
