using ExmoNet.Application.Services;
using FluentAssertions;

namespace ExmoNet.Application.UnitTests.Public;

public class ExmoPublicApiTests
{
    private const string FirstCurrency = "BTC";
    private const string SecondCurrency = "USD";

    [Fact]
    private async Task GetTradesAsyncTest()
    {
        var api = new ExmoPublicApi();
        var trades = await api.GetTradesAsync(FirstCurrency, SecondCurrency);
        trades.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    private async Task GetOrderBookAsyncTest()
    {
        var api = new ExmoPublicApi();
        const int limit = 10;
        var orderBook = await api.GetOrderBookAsync(FirstCurrency, SecondCurrency, limit);
        orderBook.Ask.Should().HaveCount(10);
    }

    [Fact]
    private async Task GetTickerAsyncTest()
    {
        var api = new ExmoPublicApi();
        var tickerElements = await api.GetTickerAsync();
        var element = tickerElements.FirstOrDefault(x =>
            x.FirstCurrency == FirstCurrency && x.SecondCurrency == SecondCurrency);
        element.Should().NotBeNull();
    }

    [Fact]
    private async Task GetPairsSettingsAsyncTest()
    {
        var api = new ExmoPublicApi();
        var pairsSettings = await api.GetPairsSettingsAsync();
        var settings = pairsSettings.FirstOrDefault(x =>
            x.FirstCurrency == FirstCurrency && x.SecondCurrency == SecondCurrency);
        settings.Should().NotBeNull();
    }

    [Fact]
    private async Task GetCurrenciesAsyncTest()
    {
        var api = new ExmoPublicApi();
        var currencies = await api.GetCurrenciesAsync();
        currencies.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    private async Task GetExtendedCurrenciesAsyncTest()
    {
        var api = new ExmoPublicApi();
        var currencies = await api.GetExtendedCurrenciesAsync();
        currencies.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    private async Task GetRequiredAmountAsyncTest()
    {
        var api = new ExmoPublicApi();
        const int quantity = 1;
        var requiredAmount = await api.GetRequiredAmountAsync(FirstCurrency, SecondCurrency, quantity);
        requiredAmount.Quantity.Should().Be(quantity);
    }

    [Fact]
    private async Task GetCandlesHistoryAsyncTest()
    {
        var api = new ExmoPublicApi();
        const int resolution = 30;
        var from = DateTime.Now.Subtract(new TimeSpan(1, 0, 0));
        var to = DateTime.Now;
        var candles = await api.GetCandlesHistoryAsync(FirstCurrency, SecondCurrency, resolution, from, to);
        candles.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    private async Task GetCryptoProvidersAsyncTest()
    {
        var api = new ExmoPublicApi();
        var providers = await api.GetCryptoProvidersAsync();
        providers.Should().HaveCountGreaterThan(0);
    }
}
