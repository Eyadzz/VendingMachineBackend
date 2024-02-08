using System.Text.Json;
using Application.Common.Tokens;
using Application.Contracts.Services;
using StackExchange.Redis;

namespace Infrastructure.Services;

public class RedisService : ICacheService
{
    private readonly IConnectionMultiplexer _redis;
    
    public RedisService(IConnectionMultiplexer redis) => _redis = redis;

    public async Task Set(TokenType token, string key, string value) => 
        await _redis.GetDatabase().StringSetAsync($"{token.Type}:{key}", value, token.ExpirationDate, flags: CommandFlags.FireAndForget);

    public async Task<string?> Get(TokenType token, string key) => await _redis.GetDatabase().StringGetAsync($"{token.Type}:{key}");

    public async Task Remove(TokenType token, string key) => await _redis.GetDatabase().KeyDeleteAsync($"{token.Type}:{key}", CommandFlags.FireAndForget);
    
    public async Task Increment(TokenType token, string key) => await _redis.GetDatabase().StringIncrementAsync($"{token.Type}:{key}", flags: CommandFlags.FireAndForget);
}