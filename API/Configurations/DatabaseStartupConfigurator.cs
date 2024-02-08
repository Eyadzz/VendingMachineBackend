using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseConfig;

namespace API.Configurations;

public static class DatabaseStartupConfigurator
{
    public static async Task ApplyMigrations(WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
    }
}
