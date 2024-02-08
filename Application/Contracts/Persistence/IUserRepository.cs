using Domain.User;

namespace Application.Contracts.Persistence;

/// <summary>
/// Represents an interface for managing user-related data in a repository.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Retrieves detailed information about a user based on their user ID.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation, yielding the <see cref="User"/> with the specified user ID.</returns>
    Task<User> GetDetails(int userId);

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="username">The email address of the user.</param>
    /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation, yielding the <see cref="User"/> with the specified email address or phone number, or null if no matching user is found.</returns>
    Task<User?> GetByUsername(string username);

    /// <summary>
    /// Checks if a username exists in the repository.
    /// </summary>
    /// <param name="username">The username to check for existence.</param>
    /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation, yielding true if the email address exists, and false otherwise.</returns>
    Task<bool> UsernameExists(string username);
    
}
