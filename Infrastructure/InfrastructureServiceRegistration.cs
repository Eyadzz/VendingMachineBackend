using System.Text;
using Application.Common.Constants;
using Application.Contracts.Authentication;
using Application.Contracts.Services;
using Infrastructure.Authentication;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(ConnectionStrings.Redis));
        services.AddSingleton<ICacheService, RedisService>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddTransient<IAuthProvider, AuthProvider>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                o => o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }
            );
        
        return services;
    }
}