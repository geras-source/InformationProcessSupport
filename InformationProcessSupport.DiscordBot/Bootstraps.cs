using DiscordBot.Services;
using InformationProcessSupport.Data.Channels;
using InformationProcessSupport.Data.Statistics;
using InformationProcessSupport.Data.Users;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot
{
    internal static class Bootstraps
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHostedService<CommandHandler>();

            return services;
        }
    }
}