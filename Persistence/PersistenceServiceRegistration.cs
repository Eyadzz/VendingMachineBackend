using Application.Common.Constants;
using Application.Contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseConfig;
using Persistence.Persistence;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ApplicationDbContext>();
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(ConnectionStrings.Postgres));

        return services;
    }
}
