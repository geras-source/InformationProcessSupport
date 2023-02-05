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
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.MicrophoneActions;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.SelfDeafenedActions;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.CameraActions;
using InformationProcessSupport.Data.TimeOfActionsInTheChannel.StreamActions;
using InformationProcessSupport.Data.Groups;
using ShellProgressBar;

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
        private readonly ISelfDeafenedActionsRepository _selfDeafenedActionsRepository;
        private readonly ICameraActionRepository _cameraActionRepository;
        private readonly IStreamActionsRepository _streamActionsRepository;
        private readonly Discord.ConnectionState _connectionState;
        private readonly IGroupReposity _groupReposity;
        private readonly Regex _regex = new Regex(@"^(\w{2}-\d{2})$");

        public CommandHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service,
                                IConfiguration config, IChannelRepository channelRepository, IUserRepository userRepository, 
                                IStatisticRepository statisticRepository, IMicrophoneActionsRepository microphoneActionsRepository,
                                ISelfDeafenedActionsRepository selfDeafenedActionsRepository, ICameraActionRepository cameraActionRepository,
                                IStreamActionsRepository streamActionsRepository, IGroupReposity groupReposity)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _config = config;
            _channelRepository = channelRepository;
            _userRepository = userRepository;
            _statisticRepository = statisticRepository;
            _microphoneActionsRepository = microphoneActionsRepository;
            _selfDeafenedActionsRepository = selfDeafenedActionsRepository;
            _cameraActionRepository = cameraActionRepository;
            _streamActionsRepository = streamActionsRepository;
            _groupReposity = groupReposity;
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
            if (_regex.IsMatch(newSocketRole.Name) && oldSocketRole.Name == "новая роль" || oldSocketRole.Name == "new role")
            {
                var entity = new GroupEntity()
                {
                    GroupName = newSocketRole.Name,
                    AlternateKey = newSocketRole.Id,
                    GuildId = newSocketRole.Guild.Id,
                    GuildName = newSocketRole.Guild.Name
                };
                await _groupReposity.AddAsync(entity);
            }
            if(_regex.IsMatch(oldSocketRole.Name) && _regex.IsMatch(newSocketRole.Name))
            {
                var groupId = await _groupReposity.GetGroupIdByAlternateId(newSocketRole.Id, newSocketRole.Guild.Id);
                var entity = new GroupEntity()
                {
                    GroupId = groupId,
                    GroupName = newSocketRole.Name
                };
                await _groupReposity.UpdateAsync(entity);
            }
        }

        private async Task OnRoleDeleted(SocketRole socketRole)
        {
            if (_regex.IsMatch(socketRole.Name))
            {
                var groupId = await _groupReposity.GetGroupIdByAlternateId(socketRole.Id, socketRole.Guild.Id);
                await _groupReposity.DeleteAsync(groupId);
            }
        }

        private async Task OnGuildMemberUpdated(Cacheable<SocketGuildUser, ulong> arg1, SocketGuildUser arg2)
        {
            var userId = await _userRepository.GetUserIdByAlternateId(alternateId: arg2.Id, guildId: arg2.Guild.Id);
            var entity = new UserEntity
            {
                UserId = userId,
                Nickname = arg2.DisplayName,
                Name = arg2.Username,
                Roles = string.Join(",", arg2.Roles.Where(x => x.Name is not "@everyone"))
            };
            await _userRepository.UpdateAsync(entity);
        }
        private Task OnClientJoinedGuild(SocketGuild arg)
        {
            throw new NotImplementedException();
        }

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
                //await data.CreateRoleAsync(name: "Test", color: Color.Teal);
                var groups = data?.Roles.Where(x => _regex.IsMatch(x.Name));
                
                if(groups.Count() != 0)
                {
                    var groupEntities = new List<GroupEntity>();
                    var current = 0;
                    using (var pbar = new ProgressBar(groups.Count(), "Saving groups"))
                    {
                        foreach (var group in groups)
                        {
                            var entity = new GroupEntity
                            {
                                GroupName = group.Name,
                                AlternateKey = group.Id,
                                GuildId = data.Id,
                                GuildName = data.Name
                            };
                            groupEntities.Add(entity);
                            pbar.Tick($"Saving groups: {++current} of {groups.Count()} from {data.Name}");
                        }
                        //await _groupReposity.AddRangeGroups(groupEntities);
                    }
                }

                var users = data?.Users;
                var usersList = new List<UserEntity>();
                using (var pbar = new ProgressBar(users.Count, "Saving users"))
                {
                    var current = 0;
                    foreach (var user in users)
                    {
                        var exists = _userRepository.ExistsAsync(user.Id, data.Id).Result;
                        if (exists == true)
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
                                GuildName = data.Name,
                                GuildId = data.Id
                            };
                            var group = user.Roles.FirstOrDefault(x => _regex.IsMatch(x.Name));
                            if (group != null)
                            {
                                entity.GroupId = await _groupReposity.GetGroupIdByAlternateId(group.Id, data.Id);
                            }
                            else
                            {
                                entity.GroupId = null;
                            }
                            usersList.Add(entity);
                        }
                        pbar.Tick($"Saving users: {++current} of {users.Count} from {data.Name}");
                    }
                    await _userRepository.AddCollectionAsync(usersList);
                }

               

                var channels = data?.Channels.Where(channel => channel is not ICategoryChannel);
                using (var pbar = new ProgressBar(channels.Count(), "Saving channels"))
                {
                    var current = 0;
                    var channelsList = new List<ChannelEntity>();
                    foreach (var channel in channels)
                    {
                        var exists = _channelRepository.ExistsAsync(channel.Id, data.Id).Result;
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
                                GuildName = data.Name,
                                GuildId = data.Id
                            };
                            channelsList.Add(entity);
                        }
                        pbar.Tick($"Saving channels: {++current} of {channels.Count()} from {data.Name}");
                    }
                    await _channelRepository.AddCollectionAsync(channelsList);
                }
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

        private async Task OnChannelUpdated(SocketChannel oldSocketChannel, SocketChannel newSocketChannel)
        {
            var channelId = await _channelRepository.GetChannelIdBuAlternateId(oldSocketChannel.Id);
            var guild = _client.Guilds.First(x => x.Channels.First(x => x.Id == newSocketChannel.Id && x.Name == newSocketChannel.ToString()) == newSocketChannel);
            var entity = new ChannelEntity
            {
                ChannelId = channelId,
                Name = newSocketChannel.ToString(),
                CategoryType = newSocketChannel.GetChannelType().ToString(),
                GuildId = guild.Id
            };
            await _channelRepository.UpdateAsync(entity);
        }

        private async Task OnChannelDestroyed(SocketChannel socketChannel)
        {
            var channelId = await _channelRepository.GetChannelIdBuAlternateId(socketChannel.Id);
            await _channelRepository.DeleteAsync(channelId);
        }

        private async Task OnChannelCreated(SocketChannel socketChannel)
        {
            var guild = _client.Guilds.First(x => x.Channels.First(x => x.Id == socketChannel.Id && x.Name == socketChannel.ToString()) == socketChannel);
            var entity = new ChannelEntity
            {
                AlternateKey = socketChannel.Id,
                CategoryType = socketChannel.GetChannelType().ToString(),
                Name = socketChannel.ToString(),
                GuildId = guild.Id,
                GuildName = guild.Name
            };
            await _channelRepository.AddAsync(entity);
        }

        private async Task OnUserLeftFromGuild(SocketGuild socketGuild, SocketUser socketUser)
        {
            var userId = await _userRepository.GetUserIdByAlternateId(alternateId: socketUser.Id, guildId: socketGuild.Id);
            await _userRepository.DeleteAsync(id: userId);
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
            await _userRepository.AddAsync(entity);
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
                Console.WriteLine($"User (Name: {user.Username} ID: {user.Id}) join a VoiceChannel (Name: {newVoiceState.VoiceChannel.Name} ID: {newVoiceState.VoiceChannel.Id})");
                //var userId = await _userRepository.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);
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
            /// User left
            /// </summary>
            if (oldVoiceState.VoiceChannel != null && newVoiceState.VoiceChannel == null | oldVoiceState.VoiceChannel.Name != newVoiceState.VoiceChannel?.Name)
            {
                Console.WriteLine($"User (Name: {user.Username} ID: {user.Id}) left from a VoiceChannel (Name: {oldVoiceState.VoiceChannel.Name} ID: {oldVoiceState.VoiceChannel.Id})");
                //var userId = await _userRepository.GetUserIdByAlternateId(user.Id, guildId: oldVoiceState.VoiceChannel.Guild.Id);
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
            ///<summary>
            /// замучен полностью (иконка с наушниками)
            /// </summary>
            if (oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsSelfDeafened == true && oldVoiceState.IsSelfDeafened == false)
            {
                Console.WriteLine("SelfDeafened");
                //var userId = await _userRepository.GetUserIdByAlternateId(alternateId: user.Id, guildId: oldVoiceState.VoiceChannel.Guild.Id);
                //var channelId = await _channelRepository.GetChannelIdBuAlternateId(oldVoiceState.VoiceChannel.Id);
                //var statisticId = _statisticRepository.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
                //var time = GetTimeAsync().Result;

                //var entity = new SelfDeafenedActionsEntity
                //{
                //    StatistisId = statisticId,
                //    SelfDeafenedTurnOffTime = time
                //};
                //await _selfDeafenedActionsRepository.AddSelfDeafenedTurnOffTimeAsync(entity);
            }
            else if(oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsSelfDeafened == false && oldVoiceState.IsSelfDeafened == true)
            {
                Console.WriteLine("UnSelfDeafened");
                //var userId = await _userRepository.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);
                //var channelId = await _channelRepository.GetChannelIdBuAlternateId(newVoiceState.VoiceChannel.Id);
                //var statisticId = _statisticRepository.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
                //var time = GetTimeAsync().Result;

                //var entity = new SelfDeafenedActionsEntity
                //{
                //    StatistisId = statisticId,
                //    SelfDeafenedTurnOnTime = time
                //};
                //await _selfDeafenedActionsRepository.AddendumASelfDeafenedOperatingTime(entity);
            }
            ///< summary >
            /// Self muted
            /// </ summary >
            if (oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsSelfMuted == true && oldVoiceState.IsSelfMuted == false
                && newVoiceState.IsSelfDeafened == false)
            {
                Console.WriteLine($"Muted :{user.Username}");
                //var userId = await _userRepository.GetUserIdByAlternateId(alternateId: user.Id, guildId: oldVoiceState.VoiceChannel.Guild.Id);
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
            else if (oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsSelfMuted == false && oldVoiceState.IsSelfMuted == true)
            {
                Console.WriteLine($"UnMuted :{user.Username}");
                //var userId = await _userRepository.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);
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
            /// Camera turn on/off
            /// </summary>
            if(oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsVideoing == true && oldVoiceState.IsVideoing == false)
            {
                Console.WriteLine($"{user.Username} turn on the camera in {newVoiceState.VoiceChannel.Name}");
                //var userId = await _userRepository.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);
                //var channelId = await _channelRepository.GetChannelIdBuAlternateId(newVoiceState.VoiceChannel.Id);
                //var statisticId = _statisticRepository.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
                //var time = GetTimeAsync().Result;

                //var entity = new CameraActionsEntity
                //{
                //    StatistisId = statisticId,
                //    CameraTurnOnTime = time
                //};

                //await _cameraActionRepository.AddCameraActionTurnOnTimeAsync(entity);
            }
            else if(oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsVideoing == false && oldVoiceState.IsVideoing == true)
            {
                Console.WriteLine($"{user.Username} turn off the camera in {newVoiceState.VoiceChannel.Name}");
                //var userId = await _userRepository.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);
                //var channelId = await _channelRepository.GetChannelIdBuAlternateId(newVoiceState.VoiceChannel.Id);
                //var statisticId = _statisticRepository.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
                //var time = GetTimeAsync().Result;

                //var entity = new CameraActionsEntity
                //{
                //    StatistisId = statisticId,
                //    CameraTurnOffTime = time
                //};

                //await _cameraActionRepository.AddendumCameraActionOperatingTime(entity);
            }
            ///<summary>
            /// Stream turn on/off
            /// </summary>
            if(oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsStreaming == true && oldVoiceState.IsStreaming == false)
            {
                Console.WriteLine($"{user.Username} turn on the stream in {newVoiceState.VoiceChannel.Name}");
                //var userId = await _userRepository.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);
                //var channelId = await _channelRepository.GetChannelIdBuAlternateId(newVoiceState.VoiceChannel.Id);
                //var statisticId = _statisticRepository.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
                //var time = GetTimeAsync().Result;

                //var entity = new StreamActionsEntity
                //{
                //    StatistisId = statisticId,
                //    StreamTurnOnTime = time
                //};
                //await _streamActionsRepository.AddStreamActionTurnOnTimeAsync(entity);
            }
            else if(oldVoiceState.VoiceSessionId == newVoiceState.VoiceSessionId && newVoiceState.IsStreaming == false && oldVoiceState.IsStreaming == true)
            {
                Console.WriteLine($"{user.Username} turn off the stream in {newVoiceState.VoiceChannel.Name}");
                //var userId = await _userRepository.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);
                //var channelId = await _channelRepository.GetChannelIdBuAlternateId(newVoiceState.VoiceChannel.Id);
                //var statisticId = _statisticRepository.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
                //var time = GetTimeAsync().Result;

                //var entity = new StreamActionsEntity
                //{
                //    StatistisId = statisticId,
                //    StreamTurnOffTime = time
                //};
                //await _streamActionsRepository.AddendumStreamActionOperatingTime(entity);
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