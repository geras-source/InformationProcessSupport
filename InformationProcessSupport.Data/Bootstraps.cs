using InformationProcessSupport.Data.Channels;
using InformationProcessSupport.Data.Statistics;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.MicrophoneActions;
using InformationProcessSupport.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InformationProcessSupport.Data
{
    public static class Bootstraps
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
               .AddScoped<IChannelRepository, ChannelRepository>()
               .AddScoped<IUserRepository, UserRepository>()
               .AddScoped<IStatisticRepository, StatisticRepository>()
               .AddScoped<IMicrophoneActionsRepository, MicrophoneActionsRepository>();

            return services;
        }
        public static IServiceCollection AddAplicationContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Transient);
            return services;
        }
    }
}