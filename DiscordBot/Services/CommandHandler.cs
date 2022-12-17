using Discord.Commands;
using Discord.WebSocket;
using Discord.Addons.Hosting;
using Discord;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Threading.Channels;

namespace DiscordBot.Services
{
    public class CommandHandler : IHostedService, IDisposable
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IConfiguration _config;

        public static List<Channels> userId = new List<Channels>();
        public static List<string> channels = new List<string>();

        public CommandHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service, IConfiguration config)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _config = config;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            _client.MessageReceived += OnMessageRecevied;
            _client.UserVoiceStateUpdated += HandleUserVoiceStateUpdated;
            _client.UserJoined += _client_UserJoined;
            _client.UserLeft += _client_UserLeft;
            _service.CommandExecuted += OnCommandExecuted;
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
            
        }

        private Task _client_UserLeft(SocketGuild arg1, SocketUser arg2)
        {
            throw new NotImplementedException();
        }

        private Task _client_UserJoined(SocketGuildUser arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> commandInfo, ICommandContext commandContext, IResult result)
        {
            if (result.IsSuccess)
            {
                return;
            }
            await commandContext.Channel.SendMessageAsync(result.ErrorReason);
        }

        private async Task OnMessageRecevied(SocketMessage socketMsg)
        {
            if (!(socketMsg is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            var argPos = 0;
            if (!message.HasStringPrefix(_config["prefix"], ref argPos) && !message.HasMentionPrefix(_client.CurrentUser, ref argPos)) return;

            var context = new SocketCommandContext(_client, message);
            await _service.ExecuteAsync(context, argPos, _provider);
        }

        private async Task HandleUserVoiceStateUpdated(SocketUser user, SocketVoiceState oldVoiceState, SocketVoiceState newVoiceState)
        {
            
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.SetGameAsync(null);
            await _client.SetStatusAsync(UserStatus.Offline);
            await _client.StopAsync();
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}