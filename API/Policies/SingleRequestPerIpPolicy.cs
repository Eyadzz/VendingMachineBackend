using System.Net;
using System.Threading.RateLimiting;
using Application.Common.Responses;
using Microsoft.AspNetCore.RateLimiting;
using RedisRateLimiting;
using StackExchange.Redis;

namespace API.Policies;

public class SingleRequestPerIpPolicy : IRateLimiterPolicy<string>
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected { get; }
    
    public SingleRequestPerIpPolicy(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
        OnRejected = async (context, _) =>
        {
            await context.HttpContext.Response.WriteAsJsonAsync(Responses.Custom("You can only make one request per minute.", HttpStatusCode.TooManyRequests));
        };
    }

    public RateLimitPartition<string> GetPartition(HttpContext httpContext)
    {
        var clientIpAddress = httpContext.Connection.RemoteIpAddress!.ToString();

        return RedisRateLimitPartition.GetFixedWindowRateLimiter(clientIpAddress, _ => new RedisFixedWindowRateLimiterOptions
        {
            PermitLimit = 1,
            ConnectionMultiplexerFactory = () => _connectionMultiplexer,
            Window = TimeSpan.FromSeconds(40)
        });
    }
}