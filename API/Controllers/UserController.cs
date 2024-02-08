using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace API.Controllers;

/// <summary>
/// Controller for managing user-related operations.
/// </summary>
public class UserController : AbstractController
{
    public UserController(IMediator mediator) : base(mediator) {}
    
    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="request">The login request containing user credentials.</param>
    /// <returns>An action result indicating the login status and response.</returns>
    //[EnableRateLimiting("FiveRequestsPerIP")]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(Login request)
    {
        var response = await Mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }
    
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="request">The registration request containing user details.</param>
    /// <returns>An action result indicating the registration status and response.</returns>
    [HttpPost("Register")]
    public async Task<IActionResult> Register(Register request)
    {
        var response = await Mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }
    
    /// <summary>
    /// Retrieves a list of user roles.
    /// </summary>
    /// <returns>An action result containing the list of user roles.</returns>
    [HttpGet("Roles")]
    public async Task<IActionResult> Roles()
    {
        var response = await Mediator.Send(new Roles());
        return StatusCode(response.StatusCode, response);
    }
    
    /// <summary>
    /// Retrieves user data.
    /// </summary>
    /// <returns>An action result containing the user's data.</returns>
    [Authorize] // Requires the user to be authenticated.
    [HttpGet("Data")]
    public async Task<IActionResult> UserData()
    {
        var response = await Mediator.Send(new UserData());
        return StatusCode(response.StatusCode, response);
    }
    
    /// <summary>
    /// Refreshes a user's access token.
    /// </summary>
    /// <param name="request">The refresh token request containing the user's refresh token.</param>
    /// <returns>An action result containing the new access token.</returns>
    /// <remarks>
    /// This endpoint is used to refresh a user's access token. The access token is used to authenticate the user for protected endpoints.
    /// </remarks>
    [EnableRateLimiting("SingleRequestPerIP")]
    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken(TokenRefresher request)
    {
        var result = await Mediator.Send(request);
        return StatusCode(result.StatusCode, result);
    }
    
    /// <summary>
    /// Deletes the user account.
    /// </summary>
    /// <returns>An IActionResult representing the result of the operation.</returns>
    [Authorize]
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete()
    {
        var response = await Mediator.Send(new DeleteUser());
        return StatusCode(response.StatusCode, response);
    }

    /// <summary>
    /// Deletes a specific refresh token associated with the user.
    /// </summary>
    /// <param name="request">The request object containing the refresh token details.</param>
    /// <returns>An IActionResult representing the result of the operation.</returns>
    [Authorize]
    [HttpDelete("DeleteRefreshToken")]
    public async Task<IActionResult> DeleteRefreshToken([FromBody] DeleteRefreshToken request)
    {
        var response = await Mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }
}