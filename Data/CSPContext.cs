using Microsoft.EntityFrameworkCore;
using PlugApi.Data.Mappings;
using PlugApi.Entities;

namespace PlugApi.Data;

public class CSPContext : DbContext
{
    protected readonly IConfiguration _configuration;

    public CSPContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerMap());
    }
}
