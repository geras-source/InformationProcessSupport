using InformationProcessSupport.Core.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InformationProcessSupport.Data
{
    public static class Bootstraps
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IStorageProvider, StorageProvider>();

            return services;
        }
        public static IServiceCollection AddApplicationContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options =>
            {
                options
                .UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Critical);
            }, ServiceLifetime.Transient);
            return services;
        }
    }
}