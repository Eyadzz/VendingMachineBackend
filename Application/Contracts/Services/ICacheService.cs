using Application.Common.Tokens;

namespace Application.Contracts.Services;

/// <summary>
/// Represents a contract for a caching service.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Sets a key-value pair in the cache based on the token type.
    /// </summary>
    /// <param name="token">The type of token for categorizing the cached data.</param>
    /// <param name="key">The key for the cache entry.</param>
    /// <param name="value">The value to be cached.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task Set(TokenType token, string key, string value);

    /// <summary>
    /// Retrieves a value from the cache based on the token type and key.
    /// </summary>
    /// <param name="token">The type of token for categorizing the cached data.</param>
    /// <param name="key">The key for the cache entry.</param>
    /// <returns>The cached value, or <c>null</c> if not found.</returns>
    Task<string?> Get(TokenType token, string key);

    /// <summary>
    /// Removes a cached value from the cache based on the token type and key.
    /// </summary>
    /// <param name="token">The type of token for categorizing the cached data.</param>
    /// <param name="key">The key for the cache entry to remove.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task Remove(TokenType token, string key);
    
    /// <summary>
    /// Increments a cached value by one from the cache based on the token type and key.
    /// </summary>
    /// <param name="token">The type of token for categorizing the cached data.</param>
    /// <param name="key">The key for the cache entry to increment.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task Increment(TokenType token, string key);
}