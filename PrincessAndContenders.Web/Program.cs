using Microsoft.EntityFrameworkCore;
using PrincessAndContenders.Data;
using PrincessAndContenders.Data.Repositories;

namespace PrincessAndContenders.Web;

static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((_, services) =>
                {
                    services.AddDbContext<DbContext, Context>();
                    services.AddScoped<ContenderRepository>();
                    services.AddScoped<AttemptRepository>();
                    services.AddScoped<SessionRepository>();
                });
                webBuilder.UseStartup<Startup>();
            });
    }
}