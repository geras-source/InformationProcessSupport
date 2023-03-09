using InformationProcessSupport.Core.ScheduleParser;
using InformationProcessSupport.Core.StatisticsCollector;
using Microsoft.Extensions.DependencyInjection;
namespace InformationProcessSupport.Core    
{
    public static class Bootstraps
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services
               .AddScoped<IParserServices, ParserServices>()
               .AddScoped<IStatisticCollectorServices, StatisticCollectorServices>();

            return services;
        }
    }
}
