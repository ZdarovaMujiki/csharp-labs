namespace PrincessAndContenders.Web;

internal class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting()
            .UseEndpoints(endpoints => endpoints.MapControllers());
    }
}