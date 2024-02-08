using System.ComponentModel.DataAnnotations;
using System.Net;
using Application.Common.Constants;
using Application.Common.Tokens;
using Application.Contracts.Authentication;
using Application.Features.Users.Commands;
using Application.Features.Users.Dto;
using Domain.User;

namespace Application.Features.Users.Queries;

public record Login : IRequest<BaseResponse>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    
}

public class LoginHandler : IRequestHandler<Login,BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthProvider _authProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ICacheService _cache;

    public LoginHandler(IUnitOfWork unitOfWork,IAuthProvider authProvider, IPasswordHasher passwordHasher, ICacheService cache)
    {
        _unitOfWork = unitOfWork;
        _authProvider = authProvider;
        _passwordHasher = passwordHasher;
        _cache = cache;
    }
    
    public async Task<BaseResponse> Handle(Login request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByUsername(request.Username);
        if (user is null)
            return Responses.InvalidCredentials();
        
        var passwordVerificationResult = _passwordHasher.Verify(request.Password, user.Password);
        if (!passwordVerificationResult)
        {
            await IncreaseLockout(request.Username);
            return Responses.InvalidCredentials();
        }
        
        if (await IsLockedOut(request.Username))
            return Responses.Invalid("Your account has been locked out");
        
        var response = new LoginResponse(AccessToken: _authProvider.AccessToken(user), RefreshToken: await GetRefreshToken(user.Id));
        
        await _cache.Remove(Tokens.Lockout, user.Id.ToString());
        
        return Responses.Success(response);
    }

    private async Task IncreaseLockout(string email)
    {
        await _cache.Increment(Tokens.Lockout, email);
    }
    
    private async Task<bool> IsLockedOut(string email)
    {
        var lockoutCount = await _cache.Get(Tokens.Lockout, email);
        return lockoutCount != null && byte.Parse(lockoutCount) >= AccountSettings.AllowedFailedAccessAttempts;
    }

    private async Task<string> GetRefreshToken(int userId)
    {
        var refreshToken = _authProvider.RefreshToken();

        await _cache.Set(Tokens.Refresh, refreshToken, userId.ToString());
        
        return refreshToken;
    }
}


