namespace Application.Common.Exceptions;

/// <summary>
/// Represents a base exception class that can be used as a foundation for custom exception types.
/// </summary>
public abstract class BaseException : Exception
{
    /// <summary>
    /// Gets or sets the HTTP status code associated with the exception.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseException"/> class with a specified error message and status code.
    /// </summary>
    /// <param name="message">The error message that describes the exception.</param>
    /// <param name="statusCode">The HTTP status code associated with the exception (default is 500 - Internal Server Error).</param>
    protected BaseException(string message, int statusCode = 500) : base(message)
    {
        StatusCode = statusCode;
    }
}
