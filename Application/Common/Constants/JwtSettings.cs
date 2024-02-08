namespace Application.Common.Constants;

public static class JwtSettings
{
    public static readonly string SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? "MK75iPxAo4XNUZXYyE8nomhiDTmiz5ON";
}