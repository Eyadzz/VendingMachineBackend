using Application.Contracts.Persistence;
using Domain.User;
using Persistence.DatabaseConfig;

namespace Persistence.Persistence;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext dbContext) : base(dbContext) {}
    public async Task<bool> Exists(byte roleId)
    {
        return await DbContext.Roles.AsNoTracking().AnyAsync(r => r.Id == roleId);
    }
}