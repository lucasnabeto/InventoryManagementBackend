using InventoryManagement.Models;

namespace InventoryManagement.Data;
public class InventoryDbContext(DbContextOptions<InventoryDbContext> options, IConfiguration configuration) : DbContext(options)
{
    private readonly IConfiguration _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        string connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        optionsBuilder.UseSqlite(connectionString);
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Sale> Sales { get; set; }
}