using IdentityServer.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Inject database context
        var connectionString = configuration.GetConnectionString("Sqlite");
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        // Inject early created db context with interface 
        services.AddScoped<IIdentityDbContext>(provider => 
            provider.GetService<IdentityDbContext>());

        return services;
    }
}