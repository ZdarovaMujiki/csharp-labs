using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace PrincessAndContenders.Data;

public class Context: DbContext
{
    public DbSet<Attempt> Attempts { get; set; }
    public DbSet<Contender> Contenders { get; set; }
    public DbSet<Session> Sessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var appSettings = ConfigurationManager.AppSettings;
        optionsBuilder.UseNpgsql(
            $"Server={appSettings["Server"]};" +
            $"Database={appSettings["Database"]};" +
            $"User Id={appSettings["User"]};" +
            $"Password={appSettings["Password"]}"
        );
    }
}