using Microsoft.EntityFrameworkCore;
using PrincessAndContenders.Data;

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
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((_, services) =>
                {
                    services.AddDbContext<DbContext, Context>();
                });
                webBuilder.UseStartup<Startup>();
            });
    }
}