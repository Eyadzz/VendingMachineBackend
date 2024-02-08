namespace Application.Common.Constants;

public static class ConnectionStrings
{
    public static readonly string Postgres = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING") ?? "Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=VendingMachine;";
    public static readonly string Redis = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") ?? "localhost:6379";
}