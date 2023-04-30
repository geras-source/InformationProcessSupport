using DiscordBot.Services;
using InformationProcessSupport.DiscordBot;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot
{
    internal static class Bootstraps
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHostedService<CommandHandler>();
            services.AddScoped<DataBaseProvider>();
            return services;
        }
    }
}