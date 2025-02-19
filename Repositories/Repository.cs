using InventoryManagement.Data;

namespace InventoryManagement.Repositories;

public class Repository<TEntity>(InventoryDbContext context) : IRepository<TEntity> where TEntity : class

{
    private readonly InventoryDbContext _context = context;

    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _context.Set<TEntity>().AsNoTracking().ToListAsync();

    public async Task<TEntity?> GetByIdAsync(int id) => await _context.Set<TEntity>().FindAsync(id);

    public async Task CreateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}