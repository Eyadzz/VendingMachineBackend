using Domain.User;

namespace Application.Contracts.Authentication;

public interface IAuthProvider
{
    /// <summary>
    /// Generates an access token for the given user
    /// </summary>
    /// <param name="user">The user to generate an access token for</param>
    /// <returns>Access token string</returns>
    string AccessToken(User user);
    
    /// <summary>
    /// Generates a refresh token for the given user
    /// </summary>
    /// <returns>Refresh token string</returns>
    string RefreshToken();
}