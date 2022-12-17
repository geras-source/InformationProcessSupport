﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Services;

var builder = new HostBuilder()
               .ConfigureAppConfiguration(x =>
               {
                   var config = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("Configurations/config.json", false, true)
                       .Build();

                   x.AddConfiguration(config);
               })
               .ConfigureLogging(x =>
               {
                   x.AddConsole();
                   x.SetMinimumLevel(LogLevel.Debug);
               })
               .ConfigureDiscordHost((context, config) =>
               {
                   config.SocketConfig = new DiscordSocketConfig
                   {
                       LogLevel = LogSeverity.Critical,
                       AlwaysDownloadUsers = false,
                       MessageCacheSize = 200,
                       GatewayIntents = GatewayIntents.All
                   };

#pragma warning disable CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.
                   config.Token = context.Configuration["token"];
#pragma warning restore CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.
               })
               .UseCommandService((context, config) =>
               {
                   config.CaseSensitiveCommands = false;
                   config.LogLevel = LogSeverity.Debug;
                   config.DefaultRunMode = RunMode.Async;
               })
               .ConfigureServices((context, services) =>
               {
                   services
                       .AddHostedService<CommandHandler>();
               })
               .UseConsoleLifetime();

var host = builder.Build();
using (host)
{
    await host.RunAsync();
}