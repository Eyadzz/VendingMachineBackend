namespace Application.Common.Tokens;

public class TokenType
{
    public TokenType(string type, TimeSpan? expirationDate)
    {
        Type = type;
        ExpirationDate = expirationDate;
    }

    public string Type { get; }
    public TimeSpan? ExpirationDate { get; }
}