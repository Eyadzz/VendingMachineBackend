using Application.Contracts.Persistence;
using Domain.User;
using Persistence.DatabaseConfig;

namespace Persistence.Persistence;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext) {}
    public async Task<User> GetDetails(int userId)
    {
        return await DbContext.Users
            .Include(u => u.Role)
            .SingleAsync(u => u.Id == userId);
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await DbContext.Users
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Username == username);
    }

    public Task<bool> UsernameExists(string username)
    {
        return DbContext.Users.AsNoTracking().AnyAsync(u => u.Username == username);
    }
}