using ExmoNet.Application.Services;

namespace ExmoNet.Cli.Commands;

[Command("trades")]
public class GetTradesCommand : ConsoleAppBase
{
    private readonly ExmoPublicApi _api;

    public GetTradesCommand(ExmoPublicApi api)
    {
        _api = api;
    }

    [Command("get")]
    public async Task GetTradesAsyncCommand(
        [Option("f", "first currency")] string firstCurrency,
        [Option("s", "second currency")] string secondCurrency)
    {
        var result = (await _api.GetTradesAsync(firstCurrency, secondCurrency)).ToArray();
        foreach (var trade in result)
        {
            Console.WriteLine(
                $"Id:{trade.TradeId}," +
                $" Unix Date:{trade.Date}," +
                $" Price:{trade.Price} {trade.SecondCurrency}," +
                $" Quantity:{trade.Quantity} {trade.FirstCurrency}," +
                $" Amount:{trade.Amount}," +
                $" Type:{trade.Type}");
        }
    }
}
