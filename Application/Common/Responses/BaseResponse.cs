using System.Net;

namespace Application.Common.Responses;

/// <summary>
/// Represents a base response structure that encapsulates data and status information for API responses.
/// </summary>
public class BaseResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseResponse"/> class with the specified data and HTTP status code.
    /// </summary>
    /// <param name="data">The response data to be included in the response.</param>
    /// <param name="statusCode">The HTTP status code associated with the response (default is 200 - OK).</param>
    public BaseResponse(object? data = null, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        StatusCode = (int)statusCode;
        Status = statusCode.ToString();
        Data = data;
    }

    /// <summary>
    /// Gets or sets the HTTP status code associated with the response.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Gets or sets the textual representation of the HTTP status code.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the response data.
    /// </summary>
    public object? Data { get; set; }
}