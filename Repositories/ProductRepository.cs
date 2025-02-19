using InventoryManagement.Data;
using InventoryManagement.Entities;

namespace InventoryManagement.Repositories;

public class ProductRepository(InventoryDbContext context) : IRepository<Product>
{
    private readonly InventoryDbContext _context = context;

    public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.AsNoTracking().ToListAsync();

    public async Task<Product?> GetByIdAsync(int id) => await _context.Products.FindAsync(id);

    public async Task CreateAsync(Product newProduct)
    {
        await _context.AddAsync(newProduct);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}