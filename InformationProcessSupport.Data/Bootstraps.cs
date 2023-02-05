using InformationProcessSupport.Data.Channels;
using InformationProcessSupport.Data.Groups;
using InformationProcessSupport.Data.Statistics;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.CameraActions;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.MicrophoneActions;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.SelfDeafenedActions;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.StreamActions;
using InformationProcessSupport.Data.Users;
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
               .AddScoped<IChannelRepository, ChannelRepository>()
               .AddScoped<IUserRepository, UserRepository>()
               .AddScoped<IStatisticRepository, StatisticRepository>()
               .AddScoped<IMicrophoneActionsRepository, MicrophoneActionsRepository>()
               .AddScoped<ISelfDeafenedActionsRepository, SelfDeafenedActionsRepository>()
               .AddScoped<ICameraActionRepository, CameraActionRepository>()
               .AddScoped<IStreamActionsRepository, StreamActionsRepository>()
               .AddScoped<IGroupReposity, GroupReposity>();

            return services;
        }
        public static IServiceCollection AddAplicationContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

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