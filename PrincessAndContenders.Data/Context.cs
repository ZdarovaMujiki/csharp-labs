using Microsoft.EntityFrameworkCore;

namespace PrincessAndContenders.Data;

public class Context: DbContext
{
    public DbSet<Attempt> Attempts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString = @"Server=localhost;Database=postgres;
                		User Id=postgres;Password=123";
        optionsBuilder.UseNpgsql(connectionString);
    }
}