using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrincessAndContenders.Interfaces;
using PrincessAndContenders.Utils;

namespace PrincessAndContenders;

static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureLogging(builder => 
                builder.AddSimpleConsole(options =>
                {
                    options.IncludeScopes = false;
                    options.SingleLine = true;
                }))
            .ConfigureServices((_, services) =>
            {
                services.AddHostedService<Princess>();
                services.AddScoped<IHall, Hall>();
                services.AddScoped<IFriend, Friend>();
                services.AddSingleton<IContendersGenerator>(new ContendersGenerator(Constants.ContendersAmount));
            });
    }
}