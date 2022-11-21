using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Interfaces;
using UserManagement.Persistence.Contexts;

namespace UserManagement.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                               == "Production"
            ? GetConnectionString()
            : configuration["DbConnection"];
        services.AddDbContext<UserManagementDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<IUserManagementDbContext, UserManagementDbContext>();
        return services;
    }
    
    private static string GetConnectionString()
    {
        var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        connectionUrl = connectionUrl!.Replace("postgres://", string.Empty);
        var userPassSide = connectionUrl.Split("@")[0];
        var hostSide = connectionUrl.Split("@")[1];
        var user = userPassSide.Split(":")[0];
        var password = userPassSide.Split(":")[1];
        var host = hostSide.Split("/")[0];
        var database = hostSide.Split("/")[1].Split("?")[0];
        return $"Host={host};Database={database};Username={user};" +
               $"Password={password};SSL Mode=Require;Trust Server Certificate=true";
    }
}