using InformationProcessSupport.Core.Channels;
using InformationProcessSupport.Core.Groups;
using InformationProcessSupport.Core.ScheduleOfSubjects;
using InformationProcessSupport.Core.Statistics;
using InformationProcessSupport.Core.TimeOfActionsInTheChannel.CameraActions;
using InformationProcessSupport.Core.TimeOfActionsInTheChannel.MicrophoneActions;
using InformationProcessSupport.Core.TimeOfActionsInTheChannel.SelfDeafenedActions;
using InformationProcessSupport.Core.TimeOfActionsInTheChannel.StreamActions;
using InformationProcessSupport.Core.Users;
using InformationProcessSupport.Data.Channels;
using InformationProcessSupport.Data.Groups;
using InformationProcessSupport.Data.ScheduleOfSubjects;
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
               .AddScoped<IGroupRepository, GroupRepository>()
               .AddScoped<IScheduleRepository, ScheduleRepository>();

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