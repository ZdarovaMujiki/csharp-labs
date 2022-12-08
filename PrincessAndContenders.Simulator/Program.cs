using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrincessAndContenders.Data;
using PrincessAndContenders.Interfaces;

namespace PrincessAndContenders.Simulator;

static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
    
    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddHostedService<Simulator>();
                services.AddDbContext<DbContext, Context>();
                services.AddScoped<IPrincess, Princess>();
                services.AddScoped<IFriend, Friend>();
                services.AddSingleton<IHall>(new Hall(new Queue<Contender>()));
            });
    }
}