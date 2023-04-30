using Discord.Commands;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using InformationProcessSupport.DiscordBot;
using InformationProcessSupport.Core.Domains;

namespace DiscordBot.Services
{
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public class CommandHandler : IHostedService, IDisposable
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IConfiguration _config;
        private readonly IStorageProvider _storageProvider;
        //private readonly Discord.ConnectionState _connectionState;
        private readonly DataBaseProvider _dataBaseProvider;
        
        public CommandHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service,
                                IConfiguration config, IStorageProvider storageProvider,
                                DataBaseProvider dataBaseProvider)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _config = config;
            _storageProvider = storageProvider;
            _dataBaseProvider = dataBaseProvider;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            await _client.StartAsync();
            await _client.SetStatusAsync(UserStatus.Online);
            _client.JoinedGuild += OnClientJoinedGuild;
            _client.SlashCommandExecuted += _client_SlashCommandExecuted;
            _client.Ready += ClientIsReady;
            _client.Disconnected += _client_Disconnected;
            _client.Connected += _client_Connected;
            
            /*_client.RoleCreated += _client_RoleCreated;*/ // неуверен, что пригодится
            _client.RoleDeleted += OnRoleDeleted;
            _client.RoleUpdated += OnRoleUpdated;

            _client.ChannelCreated += OnChannelCreated;
            _client.ChannelDestroyed += OnChannelDestroyed;
            _client.ChannelUpdated += OnChannelUpdated;
            //TODO: Доделать команды создание/обработка команд
            _client.MessageReceived += OnMessageRecevied;
            _client.UserVoiceStateUpdated += HandleUserVoiceStateUpdated;
            _client.UserJoined += OnUserJoinedToGuild;
            _client.UserLeft += OnUserLeftFromGuild;
            _client.GuildMemberUpdated += OnGuildMemberUpdated;
            _service.CommandExecuted += OnCommandExecuted;

            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task OnRoleUpdated(SocketRole oldSocketRole, SocketRole newSocketRole)
        {
            await _dataBaseProvider.UpdateRole(oldSocketRole, newSocketRole);
        }

        private async Task OnRoleDeleted(SocketRole socketRole)
        {
            await _dataBaseProvider.DeleteRole(socketRole);
        }

        private async Task OnGuildMemberUpdated(Cacheable<SocketGuildUser, ulong> arg1, SocketGuildUser arg2)
        {
            await _dataBaseProvider.UpdateUser(arg1, arg2);
        }

        private async Task OnClientJoinedGuild(SocketGuild arg)
        {
            var guild = _client.Guilds;
            foreach (var data in guild)
            {
                await _dataBaseProvider.SavingTheUsers(data);
                await _dataBaseProvider.SavingTheChannels(data);
                await _dataBaseProvider.SavingTheGroups(data);
            }
        }

        private async Task ClientIsReady()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var guild = _client.Guilds;
            foreach (var data in guild)
            {
                await _dataBaseProvider.SavingTheUsers(data);
                await _dataBaseProvider.SavingTheChannels(data);
                //// Let's build a guild command! We're going to need a guild so lets just put that in a variable.


                //// Next, lets create our slash command builder. This is like the embed builder but for slash commands.
                //var guildCommand = new SlashCommandBuilder();

                //// Note: Names have to be all lowercase and match the regular expression ^[\w-]{3,32}$
                //guildCommand.WithName("first-command");

                //// Descriptions can have a max length of 100.
                //guildCommand.WithDescription("This is my first guild slash command!");

                //// Let's do our global command
                //var globalCommand = new SlashCommandBuilder();
                //globalCommand.WithName("first-global-command");
                //globalCommand.WithDescription("This is my first global slash command");

                //try
                //{
                //    // Now that we have our builder, we can call the CreateApplicationCommandAsync method to make our slash command.
                //    await data.CreateApplicationCommandAsync(guildCommand.Build());

                //    // With global commands we don't need the guild.
                //    //await data.CreateGlobalApplicationCommandAsync(globalCommand.Build());
                //    //// Using the ready event is a simple implementation for the sake of the example. Suitable for testing and development.
                //    //// For a production bot, it is recommended to only run the CreateGlobalApplicationCommandAsync() once for each command.
                //}
                //catch (ApplicationCommandException exception)
                //{
                //    // If our command was invalid, we should catch an ApplicationCommandException. This exception contains the path of the error as well as the error message. You can serialize the Error field in the exception to get a visual of where your error is.
                //    var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);

                //    // You can send this error somewhere or just print it to the console, for this example we're just going to print it.
                //    Console.WriteLine(json);
                //}
                //var guildCommand = new SlashCommandBuilder();

                //// Note: Names have to be all lowercase and match the regular expression ^[\w-]{3,32}$
                //guildCommand.WithName("help");

                //// Descriptions can have a max length of 100.
                //guildCommand.WithDescription("This is my first guild slash command!");

                //// Let's do our global command
                //var globalCommand = new SlashCommandBuilder();
                //globalCommand.WithName("first-global-command");
                //globalCommand.WithDescription("This is my first global slash command");

                //try
                //{
                //    // Now that we have our builder, we can call the CreateApplicationCommandAsync method to make our slash command.
                //    //await data.CreateApplicationCommandAsync(guildCommand.Build());

                //    // With global commands we don't need the guild.
                //    await _client.CreateGlobalApplicationCommandAsync(globalCommand.Build());
                //    // Using the ready event is a simple implementation for the sake of the example. Suitable for testing and development.
                //    // For a production bot, it is recommended to only run the CreateGlobalApplicationCommandAsync() once for each command.
                //}
                //catch (ApplicationCommandException exception)
                //{
                //    // If our command was invalid, we should catch an ApplicationCommandException. This exception contains the path of the error as well as the error message. You can serialize the Error field in the exception to get a visual of where your error is.
                //    var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);

                //    // You can send this error somewhere or just print it to the console, for this example we're just going to print it.
                //    Console.WriteLine(json);
                //}
                //await data.CreateRoleAsync(name: "Test", color: Color.Teal);
            }
            stopWatch.Stop();
            TimeSpan timeSpan = stopWatch.Elapsed;
            Console.WriteLine($"Exection time: {timeSpan:hh\\:mm\\:ss\\:fff}\n");
        }
        private async Task _client_Connected()
        {
            Console.WriteLine(_client.ConnectionState);
        }

        private async Task _client_Disconnected(Exception arg)
        {
            Console.WriteLine(_client.ConnectionState);
        }

        private async Task _client_SlashCommandExecuted(SocketSlashCommand arg)
        {
            await arg.RespondAsync($"You executed {arg.Data.Name}", ephemeral: true);
            //var embed = new EmbedBuilder()
            //    .WithColor(Color.DarkPurple)
            //    .WithTitle("Information")
            //   ;

            ////await arg.DeferAsync();
            //await arg.RespondAsync(embed: embed.Build(), ephemeral: true);
            //Thread.Sleep(1000);
            //var embed1 = new EmbedBuilder()
            //    .WithColor(Color.DarkPurple)
            //    .WithTitle("Information")
            //    .WithImageUrl(_client.GetUser(266642180720820224).GetAvatarUrl())
            //    .AddField("Комманды", "!test 'Channel name'\n !mute \n", false)
            //    .AddField("Created by:", _client.GetUser(266642180720820224), true)
            //    .AddField("Powered by:", ".NET Framework", true);
            //await arg.ModifyOriginalResponseAsync(x => x.Embed = embed1.Build());
        }

        private async Task OnChannelUpdated(SocketChannel oldSocketChannel, SocketChannel newSocketChannel)
        {
            var guild = _client.Guilds
                .First(x => x.Channels.First(channel => channel.Id == newSocketChannel.Id && channel.Name == newSocketChannel.ToString()) == newSocketChannel);
            await _dataBaseProvider.UpdateChannel(oldSocketChannel, newSocketChannel, guild);
        }

        private async Task OnChannelDestroyed(SocketChannel socketChannel)
        {
            var channelId = await _storageProvider.GetChannelIdByAlternateId(socketChannel.Id);
            await _storageProvider.DeleteChannelAsync(channelId);
        }

        private async Task OnChannelCreated(SocketChannel socketChannel)
        {
            var guild = _client.Guilds.First(x => x.Channels.First(channel => channel.Id == socketChannel.Id && channel.Name == socketChannel.ToString()) == socketChannel);
            var entity = new ChannelEntity
            {
                AlternateKey = socketChannel.Id,
                CategoryType = socketChannel.GetChannelType().ToString(),
                Name = socketChannel.ToString() ?? "Default",
                GuildId = guild.Id,
                GuildName = guild.Name
            };
            await _storageProvider.AddChannelAsync(entity);
        }

        private async Task OnUserLeftFromGuild(SocketGuild socketGuild, SocketUser socketUser)
        {
            var userId = await _storageProvider.GetUserIdByAlternateId(alternateId: socketUser.Id, guildId: socketGuild.Id);
            await _storageProvider.DeleteUserAsync(id: userId);
        }

        private async Task OnUserJoinedToGuild(SocketGuildUser socketGuildUser)
        {
            var entity = new UserEntity
            {
                AlternateKey = socketGuildUser.Id,
                GuildId = socketGuildUser.Guild.Id,
                GuildName = socketGuildUser.Guild.Name,
                Name = socketGuildUser.Username,
                Nickname = socketGuildUser.DisplayName,
                Roles = null
            };
            await _storageProvider.AddUserAsync(entity);
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
            if (socketMsg is not SocketUserMessage message) return;
            if (message.Source != MessageSource.User) return;

            var argPos = 0;
            if (!message.HasStringPrefix(_config["prefix"], ref argPos) && !message.HasMentionPrefix(_client.CurrentUser, ref argPos)) return;

            var context = new SocketCommandContext(_client, message);
            await _service.ExecuteAsync(context, argPos, _provider);
        }

        
        private async Task HandleUserVoiceStateUpdated(SocketUser user, SocketVoiceState oldVoiceState, SocketVoiceState newVoiceState)
        {
            ///<summary>
            ///User joined
            ///</summary>>
            if (newVoiceState.VoiceChannel != null && oldVoiceState.VoiceChannel == null | oldVoiceState.VoiceChannel?.Name != newVoiceState.VoiceChannel.Name)
            {
                await _dataBaseProvider.CreateStatistic(user, newVoiceState);
            }
            ///<summary>
            /// User left
            /// </summary>
            if (oldVoiceState.VoiceChannel != null && newVoiceState.VoiceChannel == null | oldVoiceState.VoiceChannel.Name != newVoiceState.VoiceChannel?.Name)
            {
                await _dataBaseProvider.UpdateStatistic(user, oldVoiceState);
            }

            ///<summary>
            /// замучен полностью (иконка с наушниками)
            /// </summary>
            if (oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsSelfDeafened && oldVoiceState.IsSelfDeafened == false)
            {
                await _dataBaseProvider.RecordTheSelfDeafened(user, oldVoiceState);
            }
            else if(oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsSelfDeafened == false && oldVoiceState.IsSelfDeafened)
            {
                await _dataBaseProvider.UpdateRecordSelfDeafened(user, newVoiceState);
            }

            ///< summary >
            /// Self muted
            /// </ summary >
            if (oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsSelfMuted && oldVoiceState.IsSelfMuted == false
                && newVoiceState.IsSelfDeafened == false)
            {
                await _dataBaseProvider.RecordTheSelfMuted(user, oldVoiceState);
            }
            else if (oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsSelfMuted == false && oldVoiceState is { IsSelfMuted: true, IsSelfDeafened: false })
            {
                await _dataBaseProvider.UpdateRecordSelfMuted(user, oldVoiceState, newVoiceState);
            }

            ///<summary>
            /// Camera turn on/off
            /// </summary>
            if(oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsVideoing && oldVoiceState.IsVideoing == false)
            {
                await _dataBaseProvider.RecordTurnOnCamera(user, newVoiceState);
            }
            else if(oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsVideoing == false && oldVoiceState.IsVideoing)
            {
                await _dataBaseProvider.RecordTurnOffCamera(user, newVoiceState);
            }
            ///<summary>
            /// Stream turn on/off
            /// </summary>
            if(oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsStreaming && oldVoiceState.IsStreaming == false)
            {
                await _dataBaseProvider.RecordTurnOnStream(user, newVoiceState);
            }
            else if(oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsStreaming == false && oldVoiceState.IsStreaming)
            {
                await _dataBaseProvider.RecordTurnOffStream(user, newVoiceState);
            }
        }

        
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.SetGameAsync("Отключение");
            await _client.SetStatusAsync(UserStatus.Offline);
            await _client.StopAsync();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}