using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot;
using InformationProcessSupport.Data;

var config = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("Configurations/config.json", false, true)
                       .Build();

var builder = new HostBuilder()
               .ConfigureAppConfiguration(x =>
               {
                   x.AddConfiguration(config);
               })
               .ConfigureLogging(x =>
               {
                   x.AddConsole();
                   x.SetMinimumLevel(LogLevel.Critical);
               })
               .ConfigureDiscordHost((context, config) =>
               {
                   config.SocketConfig = new DiscordSocketConfig
                   {
                       LogLevel = LogSeverity.Critical,
                       AlwaysDownloadUsers = false,
                       MessageCacheSize = 200,
                       GatewayIntents = GatewayIntents.All,
                       UseInteractionSnowflakeDate = false,
                       HandlerTimeout = 100000000,
                       ConnectionTimeout = 100000
                   };

#pragma warning disable CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.
                   config.Token = context.Configuration["token"];
#pragma warning restore CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.
               })
               .UseCommandService((context, config) =>
               {
                   config.CaseSensitiveCommands = false;
                   config.LogLevel = LogSeverity.Critical;
                   config.DefaultRunMode = RunMode.Async;
               })
               .ConfigureServices((context, services) =>
               {
                   services
                       .AddServices()
                       .AddRepositories()
                       .AddApplicationContext(config.GetSection("ConnectionStrings"));
               })
               .UseConsoleLifetime();

var host = builder.Build();
using (host)
{
    await host.RunAsync();
}