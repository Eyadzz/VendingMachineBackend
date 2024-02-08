using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Contracts.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId => Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Sid));
    public string Email => _httpContextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Email)!;
    public List<string> Roles => _httpContextAccessor.HttpContext?.User.Claims
        .Where(claim => claim.Type == "role")
        .Select(claim => claim.Value)
        .ToList() ?? new List<string>();

    public bool HasRole(string role) => Roles.Contains(role);
}