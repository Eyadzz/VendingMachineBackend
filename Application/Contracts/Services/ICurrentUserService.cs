namespace Application.Contracts.Services;


/// <summary>
/// Holds information about the current user
/// </summary>
/// <remarks>
/// This class stores information extracted from the token of the user, such as their ID and email and list of roles.
/// </remarks>
public interface ICurrentUserService
{
    /// <summary>
    /// User's unique identifier.
    /// </summary>
    int UserId { get; }
    
    /// <summary>
    /// User's unique Email.
    /// </summary>
    string Email { get; }
    
    /// <summary>
    /// User's set of Roles.
    /// </summary>
    List<string> Roles { get; }

    /// <summary>
    /// Checks if the user has the given role
    /// </summary>
    /// <param name="role">Role to check if exists</param>
    bool HasRole(string role);
    
}
