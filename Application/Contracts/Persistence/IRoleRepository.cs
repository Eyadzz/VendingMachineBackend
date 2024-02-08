using Domain.User;

namespace Application.Contracts.Persistence;

/// <summary>
/// Represents an interface for managing roles in a repository.
/// </summary>
public interface IRoleRepository : IBaseRepository<Role>
{
    /// <summary>
    /// Checks if a role with the specified role ID exists in the repository.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role to check for existence.</param>
    /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation, yielding true if the role with the specified ID exists, and false otherwise.</returns>
    Task<bool> Exists(byte roleId);
}