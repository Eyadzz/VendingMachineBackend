namespace Application.Common.Tokens;

public static class Tokens
{
    public static TokenType Refresh => new("Refresh", TimeSpan.FromDays(7));
    public static TokenType Lockout => new("Lockout", null);
}