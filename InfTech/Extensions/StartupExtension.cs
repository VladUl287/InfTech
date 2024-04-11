using Microsoft.EntityFrameworkCore;

namespace InfTech.MVC.Extensions;

public static class StartupExtensions
{
    public static void AddDatabase<TContext, TAssemblyMarker>(this IServiceCollection services, IConfiguration configuration)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(
            options =>
            {
                //options.UseInMemoryDatabase("obl");
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    options =>
                    {
                        options.MigrationsAssembly(typeof(TAssemblyMarker).Assembly.FullName);
                    });
                options.LogTo(Console.WriteLine);
                options.EnableSensitiveDataLogging();
            },
            ServiceLifetime.Scoped
        );
    }
}
