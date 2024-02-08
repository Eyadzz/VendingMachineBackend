using Application.Contracts.Persistence;
using Persistence.DatabaseConfig;

namespace Persistence.Persistence;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly ApplicationDbContext DbContext;

    protected BaseRepository(ApplicationDbContext dbContext) => DbContext = dbContext;

    public virtual async Task<T?> GetByIdAsync(int id) => await DbContext.Set<T>().FindAsync(id);

    public async Task<ICollection<T>> ListAllAsync() => await DbContext.Set<T>().ToListAsync();
    
    public void Delete(T entity) => DbContext.Set<T>().Remove(entity);
    
    public async Task<T> AddAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
        return entity;
    }
}
