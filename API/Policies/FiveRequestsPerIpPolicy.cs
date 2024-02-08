using System.Net;
using System.Threading.RateLimiting;
using Application.Common.Responses;
using Microsoft.AspNetCore.RateLimiting;
using RedisRateLimiting;
using StackExchange.Redis;

namespace API.Policies;

public class FiveRequestsPerIpPolicy : IRateLimiterPolicy<string>
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected { get; }
    
    public FiveRequestsPerIpPolicy(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
        OnRejected = async (context, _) =>
        {
            await context.HttpContext.Response.WriteAsJsonAsync(Responses.Custom("You can only make five requests per minute.", HttpStatusCode.TooManyRequests));
        };
    }

    public RateLimitPartition<string> GetPartition(HttpContext httpContext)
    {
        var clientIpAddress = httpContext.Connection.RemoteIpAddress!.ToString();

        return RedisRateLimitPartition.GetFixedWindowRateLimiter(clientIpAddress, _ => new RedisFixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            ConnectionMultiplexerFactory = () => _connectionMultiplexer,
            Window = TimeSpan.FromMinutes(1)
        });
    }
}