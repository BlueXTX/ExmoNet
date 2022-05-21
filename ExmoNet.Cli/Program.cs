using Microsoft.Extensions.Hosting;

namespace ExmoNet.Cli;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder()
            .ConfigureServices(services => { })
            .RunConsoleAppFrameworkAsync(args);
    }
}
