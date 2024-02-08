using System.Data;
using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.DatabaseConfig;
namespace Persistence.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Users = new UserRepository(_dbContext);
        Roles = new RoleRepository(_dbContext);
        Products = new ProductsRepository(_dbContext);
    }

    public IUserRepository Users { get; set; }
    public IRoleRepository Roles { get; set; }
    public IProductsRepository Products { get; set; }
    public async Task Save() => await _dbContext.SaveChangesAsync();

    public async Task BeginTransaction() => _transaction = await _dbContext.Database.BeginTransactionAsync();

    public async Task BeginTransaction(IsolationLevel isolationLevel) => _transaction = await _dbContext.Database.BeginTransactionAsync(isolationLevel);

    public async Task Commit()
    {
        if (_transaction != null)
            await _transaction.CommitAsync();
    }

    public async Task Rollback()
    {
        if (_transaction != null)
            await _transaction.RollbackAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        _transaction?.Dispose();
    }
}