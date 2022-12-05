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
            // .ConfigureLogging(builder => 
                // builder.AddSimpleConsole(options =>
                // {
                    // options.IncludeScopes = false;
                    // options.SingleLine = true;
                // }))
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
            // .ConfigureServices((_, services) =>
            // {
                // services.AddHostedService<Princess>();
                // services.AddScoped<Hall>();
                // services.AddScoped<Friend>();
            // });
    }
}