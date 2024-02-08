using System.Net;

namespace Application.Common.Responses;

public static class Responses
{
    public static BaseResponse NotFound(string entity) => new($"{entity} Not Found", HttpStatusCode.NotFound);

    public static BaseResponse Success() => new();

    public static BaseResponse Success(object data) => new(data);

    public static BaseResponse AlreadyExist(string entity) => new($"{entity} Already Exists", HttpStatusCode.Conflict);

    public static BaseResponse Unauthorized() => new("Unauthorized", statusCode: HttpStatusCode.Unauthorized);
    
    public static BaseResponse InvalidCredentials() => new("Invalid Credentials", statusCode: HttpStatusCode.Unauthorized);
    
    public static BaseResponse Invalid(string message) => new(message, HttpStatusCode.NotAcceptable);
    
    public static BaseResponse Custom(string message, HttpStatusCode statusCode) => new(message, statusCode);
}