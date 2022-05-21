using System.Text.Json;
using ExmoNet.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExmoNet.Cli;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder()
            .ConfigureServices(services => { services.AddScoped<ExmoPublicApi>(); })
            .RunConsoleAppFrameworkAsync(args);
    }
}
