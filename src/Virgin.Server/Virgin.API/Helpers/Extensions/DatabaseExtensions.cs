using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Virgin.API.Helpers.Extensions
{
    public static class DatabaseExtensions
    {
        public static void MigrateDatabase<T>(this IApplicationBuilder app) where T : DbContext
        {
            using var serviceScope = app.ApplicationServices
                                        .GetRequiredService<IServiceScopeFactory>()
                                        .CreateScope();

            using var context = serviceScope.ServiceProvider
                                            .GetService<T>();
            context.Database.Migrate();
        }
    }
}