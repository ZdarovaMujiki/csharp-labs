using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PrincessAndContenders.Client;

static class Program
{
    public static async Task Main(string[] args)
    {
        await Api.ResetSession();
        
        var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.ReceiveEndpoint("name-created-event", e =>
            {
                e.Consumer<NextContenderConsumer>();
            });
        });
        
        await busControl.StartAsync();
        
        CreateHostBuilder(args).Build().Run();

        await busControl.StopAsync();
    }
    
    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddMassTransit(configurator =>
                {
                    configurator.UsingRabbitMq();
                    configurator.AddSingleton(new Princess());
                    configurator.AddConsumer<NextContenderConsumer>();
                });
            });
    }
}