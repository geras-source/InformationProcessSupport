using Discord.Commands;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Data;
using InformationProcessSupport.Data.Channels;
using InformationProcessSupport.Data.Users;
using InformationProcessSupport.Data.Statistics;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Discord.Net;
using Newtonsoft.Json;
using DiscordBot.Modules;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.MicrophoneActions;

namespace DiscordBot.Services
{
    public class CommandHandler : IHostedService, IDisposable
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IConfiguration _config;
        private readonly IChannelRepository _channelRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStatisticRepository _statisticRepository;
        private readonly IMicrophoneActionsRepository _microphoneActionsRepository;

        public CommandHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service,
                                IConfiguration config, IChannelRepository channelRepository, IUserRepository userRepository, 
                                IStatisticRepository statisticRepository, IMicrophoneActionsRepository microphoneActionsRepository)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _config = config;
            _channelRepository = channelRepository;
            _userRepository = userRepository;
            _statisticRepository = statisticRepository;
            _microphoneActionsRepository = microphoneActionsRepository;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            await _client.StartAsync();
            await _client.SetStatusAsync(UserStatus.Online);
            _client.Ready += ClientIsReady;
            //_client.ChannelCreated += _client_ChannelCreated;
            //_client.JoinedGuild += _client_JoinedGuild;
            //_client.ChannelDestroyed += _client_ChannelDestroyed;
            //_client.ChannelUpdated += _client_ChannelUpdated;
            _client.MessageReceived += OnMessageRecevied;
            _client.UserVoiceStateUpdated += HandleUserVoiceStateUpdated;
            //_client.UserJoined += _client_UserJoined;
            //_client.UserLeft += _client_UserLeft;
            _service.CommandExecuted += OnCommandExecuted;
            _client.SlashCommandExecuted += _client_SlashCommandExecuted;

            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task _client_SlashCommandExecuted(SocketSlashCommand arg)
        {
            var embed = new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithTitle("Information")
               ;

            await arg.DeferAsync();
            await arg.ModifyOriginalResponseAsync(x => x.Embed = embed.Build());
            Thread.Sleep(1000);
            var embed1 = new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithTitle("Information")
                .WithImageUrl(_client.GetUser(266642180720820224).GetAvatarUrl())
                .AddField("Комманды", "!test 'Channel name'\n !mute \n", false)
                .AddField("Created by:", _client.GetUser(266642180720820224), true)
                .AddField("Powered by:", ".NET Framework", true);
            await arg.ModifyOriginalResponseAsync(x => x.Embed = embed1.Build());
        }

        //private Task _client_ChannelUpdated(SocketChannel arg1, SocketChannel arg2)
        //{
        //    throw new NotImplementedException();
        //}

        //private Task _client_ChannelDestroyed(SocketChannel arg)
        //{
        //    throw new NotImplementedException();
        //}

        //private Task _client_ChannelCreated(SocketChannel arg)
        //{
        //    throw new NotImplementedException();
        //}

        //private Task _client_JoinedGuild(SocketGuild arg)
        //{
        //    throw new NotImplementedException();
        //}

        private async Task ClientIsReady()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var guild = _client.Guilds;

            foreach (var data in guild)
            {
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
                var users = data?.Users;
                var usersList = new List<UserEntity>();
                foreach (var user in users)
                {
                    var exists = _userRepository.ExistsAsync(user.Id, user.Guild.Name).Result;
                    if(exists == true)
                    {
                        continue;
                    }
                    else
                    {
                        var entity = new UserEntity
                        {
                            AlternateKey = user.Id,
                            Name = user.Username,
                            Nickname = user?.DisplayName,
                            Roles = string.Join(",", user.Roles.Where(x => x.Name is not "@everyone")),
                            GuildName = data.Name
                        };
                        usersList.Add(entity);
                    }
                }
                await _userRepository.AddCollectionAsync(usersList);

                var channels = data?.Channels.Where(channel => channel is not ICategoryChannel);
                var channelsList = new List<ChannelEntity>();
                foreach (var channel in channels)
                {
                    var exists = _channelRepository.ExistsAsync(channel.Id, channel.Guild.Name).Result;
                    if (exists == true)
                    {
                        continue;
                    }
                    else
                    {
                        var entity = new ChannelEntity
                        {
                            AlternateKey = channel.Id,
                            Name = channel.Name,
                            CategoryType = channel.GetChannelType().ToString(),
                            GuildName = data.Name
                        };
                        channelsList.Add(entity);
                    }
                }
                await _channelRepository.AddCollectionAsync(channelsList);
            }
            stopWatch.Stop();
            TimeSpan timeSpan = stopWatch.Elapsed;
            Console.WriteLine($"Exection time: {timeSpan:hh\\:mm\\:ss\\:fff}\n");
        }

        //private Task _client_UserLeft(SocketGuild arg1, SocketUser arg2)
        //{
        //    throw new NotImplementedException();
        //}

        //private Task _client_UserJoined(SocketGuildUser arg)
        //{
        //    Console.WriteLine(arg);
        //    return Task.CompletedTask;
        //}

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
                Console.WriteLine($"User (Name: {user.Username} ID: {user.Id}) join a VoiceChannel (Name: {newVoiceState.VoiceChannel.Name} ID: {newVoiceState.VoiceChannel.Id})");
                //var userId = await _userRepository.GetUserIdByAlternateId(alternateId: user.Id, guildName: newVoiceState.VoiceChannel.Guild.Name);
                //var channelId = await _channelRepository.GetChannelIdBuAlternateId(newVoiceState.VoiceChannel.Id);
                //var entity = new StatisticEntity
                //{
                //    UserId = userId,
                //    ChannelId = channelId,
                //    EntryTime = GetTimeAsync().Result
                //};

                //await _statisticRepository.AddAsync(entity);
            }
            ///<summary>
            /// замучен полностью (иконка с наушниками)
            /// if (oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsSelfDeafened == true) Console.WriteLine("YES");
            /// </summary>

            ///< summary >
            /// Self muted
            /// </ summary >
            if (oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsSelfMuted == true && oldVoiceState.IsSelfMuted == false)
            {
                Console.WriteLine($"Muted :{user.Username}");
                //var userId = await _userRepository.GetUserIdByAlternateId(alternateId: user.Id, guildName: oldVoiceState.VoiceChannel.Guild.Name);
                //var channelId = await _channelRepository.GetChannelIdBuAlternateId(oldVoiceState.VoiceChannel.Id);
                //var statisticId = _statisticRepository.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
                //var time = GetTimeAsync().Result;

                //var entity = new MicrophoneActionsEntity
                //{
                //    StatistisId = statisticId,
                //    MicrophoneTurnOffTime = time
                //};
                //await _microphoneActionsRepository.AddMicrophoneTurnOffTimeAsync(entity);
            }
            ///< summary >
            /// Un self muted
            /// </ summary >
            else if (oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsSelfMuted == false && oldVoiceState.IsSelfMuted == true)
            {
                Console.WriteLine($"UnMuted :{user.Username}");
                //var userId = await _userRepository.GetUserIdByAlternateId(alternateId: user.Id, guildName: newVoiceState.VoiceChannel.Guild.Name);
                //var channelId = await _channelRepository.GetChannelIdBuAlternateId(oldVoiceState.VoiceChannel.Id);
                //var statisticId = _statisticRepository.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
                //var time = GetTimeAsync().Result;

                //var entity = new MicrophoneActionsEntity
                //{
                //    StatistisId = statisticId,
                //    MicrophoneTurnOnTime = time
                //};
                //await _microphoneActionsRepository.AddendumAMicrophoneOperatingTime(entity);
            }
            ///<summary>
            /// User left
            /// </summary>
            if (oldVoiceState.VoiceChannel != null && newVoiceState.VoiceChannel == null)
            {
                Console.WriteLine($"User (Name: {user.Username} ID: {user.Id}) left from a VoiceChannel (Name: {oldVoiceState.VoiceChannel.Name} ID: {oldVoiceState.VoiceChannel.Id})");
                //var userId = await _userRepository.GetUserIdByAlternateId(user.Id, guildName: oldVoiceState.VoiceChannel.Guild.Name);
                //var channelId = await _channelRepository.GetChannelIdBuAlternateId(oldVoiceState.VoiceChannel.Id);
                //var time = GetTimeAsync().Result;

                //var entity = new StatisticEntity
                //{
                //    UserId = userId,
                //    ChannelId = channelId,
                //    ExitTime = time
                //};
                //await _statisticRepository.UpdateConnectionTimeAsync(entity);
            }
        }

        private static async Task<DateTime> GetTimeAsync()
        {
            var time = DateTime.Now;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    await socket.ConnectAsync("time.nist.gov", 13);
                    using StreamReader rstream = new StreamReader(new NetworkStream(socket));
                    string value = rstream.ReadToEnd().Trim();
                    MatchCollection matches = Regex.Matches(value, @"((\d*)-(\d*)-(\d*))|((\d*):(\d*):(\d*))");
                    string[] dd = matches[0].Value.Split('-');
                    time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse($"{matches[1].Value} {dd[2]}.{dd[1]}.{dd[0]}"),
                        TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time"));
                }
                catch
                {
                    return time; //fail get date and time in network internet
                }
            }
            return time;
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.SetGameAsync("Отключение");
            await _client.SetStatusAsync(UserStatus.Offline);
            await _client.StopAsync();
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}