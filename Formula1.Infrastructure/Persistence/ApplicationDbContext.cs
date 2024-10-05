using Formula1.Domain.Entities;
using Formula1.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Formula1.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
    : DbContext(options)
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

    public DbSet<Circuit> FORMULA1_Circuits { get; set; }

    public DbSet<Race> FORMULA1_Races { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CircuitConfiguration.Configure(modelBuilder.Entity<Circuit>());
        RaceConfiguration.Configure(modelBuilder.Entity<Race>());

        base.OnModelCreating(modelBuilder);
    }
}
