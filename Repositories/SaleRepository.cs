using InventoryManagement.Data;
using InventoryManagement.Entities;
using SQLitePCL;

namespace InventoryManagement.Repositories;

public class SaleRepository(InventoryDbContext context) : IRepository<Sale>
{
    private readonly InventoryDbContext _context = context;

    public async Task<IEnumerable<Sale>> GetAllAsync() => await _context.Sales.AsNoTracking().ToListAsync();

    public async Task<Sale?> GetByIdAsync(int id) => await _context.Sales.FindAsync(id);

    public async Task CreateAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Sale sale)
    {
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Sale sale)
    {
        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
    }
}