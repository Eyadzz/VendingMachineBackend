namespace Application.Features.Users.Dto;

public record LoginResponse(string AccessToken, string RefreshToken);