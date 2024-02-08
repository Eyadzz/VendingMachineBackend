using Application.Contracts.Persistence;
using Domain.VendingMachine;
using Persistence.DatabaseConfig;

namespace Persistence.Persistence;

public class ProductsRepository : BaseRepository<Product>, IProductsRepository
{
    public ProductsRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}