using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simulator.Application;
using Simulator.Core.Interfaces;
using Simulator.Infrastructure.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var serviceProvider = ConfigureServices();
        var runner = serviceProvider.GetRequiredService<SimulationRunner>();
        await runner.RunAsync();
        Console.WriteLine("Simulation completed.");
    }

    private static ServiceProvider ConfigureServices()
    {
        var configuration = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .Build();

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddHttpClient<ICustomerApiClient, CustomerApiClient>(client =>
        {
            var baseUrl = configuration.GetValue<string>("ApiBaseUrl");
            client.BaseAddress = new Uri(baseUrl);
        });
        services.AddSingleton<ICustomerGenerator, CustomerGenerator>();
        services.AddSingleton<SimulationRunner>();
        return services.BuildServiceProvider();
    }
}
