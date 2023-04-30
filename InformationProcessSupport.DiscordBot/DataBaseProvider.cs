using Discord.WebSocket;
using System.Text.RegularExpressions;
using InformationProcessSupport.Core.Domains;
using Discord;
using InformationProcessSupport.Data.Models;
using ShellProgressBar;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Net.Sockets;

namespace InformationProcessSupport.DiscordBot
{
    public class DataBaseProvider
    {
        private readonly IStorageProvider _storageProvider;
        private readonly Regex _regex = new(@"^(\w{3}-\d{2})$");

        public DataBaseProvider(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }
        /// <summary>
        /// This method updates an existing role <see cref="SocketRole"/> (in this case, a group) in the database, or adds a new one when creating it in the discord
        /// </summary>
        /// <param name="oldSocketRole">Old socket role in discord</param>
        /// <param name="newSocketRole">New socket role in discord</param>
        public async Task UpdateRole(SocketRole oldSocketRole, SocketRole newSocketRole)
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
                await _storageProvider.AddGroupAsync(entity);
            }
            else if (_regex.IsMatch(oldSocketRole.Name) && _regex.IsMatch(newSocketRole.Name))
            {
                var groupId = await _storageProvider.GetGroupIdByAlternateId(newSocketRole.Id, newSocketRole.Guild.Id);
                var entity = new GroupEntity()
                {
                    GroupId = groupId,
                    GroupName = newSocketRole.Name
                };
                await _storageProvider.UpdateGroupAsync(entity);
            }
        }
        /// <summary>
        /// This method removes a role-based <see cref="SocketRole"/> group from the database
        /// </summary>
        /// <param name="socketRole">The role being deleted</param>
        public async Task DeleteRole(SocketRole socketRole)
        {
            if (_regex.IsMatch(socketRole.Name))
            {
                var groupId = await _storageProvider.GetGroupIdByAlternateId(socketRole.Id, socketRole.Guild.Id);
                await _storageProvider.DeleteGroupAsync(groupId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public async Task UpdateUser(Cacheable<SocketGuildUser, ulong> arg1, SocketGuildUser arg2)
        {
            var userId = await _storageProvider.GetUserIdByAlternateId(alternateId: arg2.Id, guildId: arg2.Guild.Id);
            var entity = new UserEntity
            {
                UserId = userId,
                Nickname = arg2.DisplayName,
                Name = arg2.Username,
                Roles = string.Join(",", arg2.Roles.Where(x => x.Name is not "@everyone"))
            };
            await _storageProvider.UpdateUserAsync(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task SavingTheUsers(SocketGuild data)
        {
            var users = data.Users;
            var usersList = new List<UserEntity>();
            using (var pbar = new ProgressBar(users.Count, "Saving users"))
            {
                var current = 0;
                foreach (var user in users)
                {
                    var exists = _storageProvider.IsUserExistsAsync(user.Id, data.Id).Result;
                    if (exists)
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
                        //var group = user.Roles.FirstOrDefault(x => _regex.IsMatch(x.Name));
                        //if (group != null)
                        //{
                        //    entity.GroupId = await _groupRepository.GetGroupIdByAlternateId(group.Id, data.Id);
                        //}
                        //else
                        //{
                        //    entity.GroupId = null;
                        //}
                        usersList.Add(entity);
                    }
                    pbar.Tick($"Saving users: {++current} of {users.Count} from {data.Name}");
                }
                await _storageProvider.AddUserCollectionAsync(usersList);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task SavingTheChannels(SocketGuild data)
        {
            var channels = data.Channels.Where(channel => channel is not ICategoryChannel);
            using (var pbar = new ProgressBar(channels.Count(), "Saving channels"))
            {
                var current = 0;
                var channelsList = new List<ChannelEntity>();
                foreach (var channel in channels)
                {
                    var exists = _storageProvider.IsChannelExistsAsync(channel.Id, data.Id).Result;
                    if (exists)
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
                await _storageProvider.AddChannelCollectionAsync(channelsList);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task SavingTheGroups(SocketGuild data)
        {
            var groups = data?.Roles.Where(x => _regex.IsMatch(x.Name));

            if (groups.Count() != 0)
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
                    await _storageProvider.AddGroupCollectionAsync(groupEntities);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldSocketChannel"></param>
        /// <param name="newSocketChannel"></param>
        /// <param name="guild"></param>
        /// <returns></returns>
        public async Task UpdateChannel(SocketChannel oldSocketChannel, SocketChannel newSocketChannel, SocketGuild guild)
        {
            var channelId = await _storageProvider.GetChannelIdByAlternateId(oldSocketChannel.Id);
            
            var entity = new ChannelEntity
            {
                ChannelId = channelId,
                Name = newSocketChannel.ToString(),
                CategoryType = newSocketChannel.GetChannelType().ToString(),
                GuildId = guild.Id
            };
            await _storageProvider.UpdateChannelAsync(entity);
        }
        private enum Attedance
        {
            Опоздал = 0,
            Неизвестно = 1,
            Вовермя = 2
        }
        public async Task CreateStatistic(SocketUser user, SocketVoiceState newVoiceState)
        {
            TimeOnly timeOnly = TimeOnly.Parse("12:20:00");
            var channelId = await _storageProvider.GetChannelIdByAlternateId(newVoiceState.VoiceChannel.Id);
            ScheduleEntity schedulemodel = null;

            schedulemodel = await _storageProvider.GetScheduleByChannelIdAsync(channelId, timeOnly);

            var attendance = Attedance.Опоздал;
            if (schedulemodel == null)
            {
                attendance = Attedance.Неизвестно;
            }
            else
            {
                var startTime = TimeOnly.FromTimeSpan(schedulemodel.StartTimeTheSubject);
                if (timeOnly >= startTime && timeOnly <= startTime.AddMinutes(15))
                {
                    attendance = Attedance.Вовермя;
                }
            }

            Console.WriteLine($"User (Name: {user.Username} ID: {user.Id}) join a VoiceChannel (Name: {newVoiceState.VoiceChannel.Name} ID: {newVoiceState.VoiceChannel.Id})");
            var userId = await _storageProvider.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);

            var entity = new StatisticEntity
            {
                UserId = userId,
                ChannelId = channelId,
                EntryTime = GetTimeAsync().Result,
                Attendance = attendance.ToString(),
                SheduleId = schedulemodel?.ScheduleId
            };

            await _storageProvider.AddStatisticAsync(entity);
        }

        public async Task UpdateStatistic(SocketUser user, SocketVoiceState oldVoiceState)
        {
            Console.WriteLine($"User (Name: {user.Username} ID: {user.Id}) left from a VoiceChannel (Name: {oldVoiceState.VoiceChannel.Name} ID: {oldVoiceState.VoiceChannel.Id})");
            var userId = await _storageProvider.GetUserIdByAlternateId(user.Id, guildId: oldVoiceState.VoiceChannel.Guild.Id);
            var channelId = await _storageProvider.GetChannelIdByAlternateId(oldVoiceState.VoiceChannel.Id);
            var time = GetTimeAsync().Result;
            // TODO: проверку на активность при выходе из канала
            var entity = new StatisticEntity
            {
                UserId = userId,
                ChannelId = channelId,
                ExitTime = time
            };
            await _storageProvider.UpdateConnectionTimeInStatisticAsync(entity);
        }

        public async Task RecordTheSelfDeafened(SocketUser user, SocketVoiceState oldVoiceState)
        {
            Console.WriteLine("SelfDeafened");
            var userId = await _storageProvider.GetUserIdByAlternateId(alternateId: user.Id, guildId: oldVoiceState.VoiceChannel.Guild.Id);
            var channelId = await _storageProvider.GetChannelIdByAlternateId(oldVoiceState.VoiceChannel.Id);
            var statisticId = _storageProvider.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
            var time = GetTimeAsync().Result;

            var entity = new SelfDeafenedActionsEntity
            {
                StatisticId = statisticId,
                SelfDeafenedTurnOffTime = time
            };
            await _storageProvider.AddSelfDeafenedTurnOffTimeAsync(entity);
        }

        public async Task UpdateRecordSelfDeafened(SocketUser user, SocketVoiceState newVoiceState)
        {
            Console.WriteLine("UnSelfDeafened");
            var userId = await _storageProvider.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);
            var channelId = await _storageProvider.GetChannelIdByAlternateId(newVoiceState.VoiceChannel.Id);
            var statisticId = _storageProvider.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
            var time = GetTimeAsync().Result;

            var entity = new SelfDeafenedActionsEntity
            {
                StatisticId = statisticId,
                SelfDeafenedTurnOnTime = time
            };
            await _storageProvider.AddendumASelfDeafenedOperatingTime(entity);
        }

        public async Task RecordTheSelfMuted(SocketUser user, SocketVoiceState oldVoiceState)
        {
            Console.WriteLine($"Muted :{user.Username}");
            var userId = await _storageProvider.GetUserIdByAlternateId(alternateId: user.Id, guildId: oldVoiceState.VoiceChannel.Guild.Id);
            var channelId = await _storageProvider.GetChannelIdByAlternateId(oldVoiceState.VoiceChannel.Id);
            var statisticId = _storageProvider.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
            var time = GetTimeAsync().Result;

            var entity = new MicrophoneActionsEntity
            {
                StatisticId = statisticId,
                MicrophoneTurnOffTime = time
            };
            await _storageProvider.AddMicrophoneTurnOffTimeAsync(entity);
        }

        public async Task UpdateRecordSelfMuted(SocketUser user, SocketVoiceState oldVoiceState, SocketVoiceState newVoiceState)
        {
            Console.WriteLine($"UnMuted :{user.Username}");
            var userId = await _storageProvider.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);
            var channelId = await _storageProvider.GetChannelIdByAlternateId(oldVoiceState.VoiceChannel.Id);
            var statisticId = _storageProvider.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
            var time = GetTimeAsync().Result;

            var entity = new MicrophoneActionsEntity
            {
                StatisticId = statisticId,
                MicrophoneTurnOnTime = time
            };
            await _storageProvider.AddendumAMicrophoneOperatingTime(entity);
        }

        public async Task RecordTurnOnCamera(SocketUser user, SocketVoiceState newVoiceState)
        {
            Console.WriteLine($"{user.Username} turn on the camera in {newVoiceState.VoiceChannel.Name}");
            var userId = await _storageProvider.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);
            var channelId = await _storageProvider.GetChannelIdByAlternateId(newVoiceState.VoiceChannel.Id);
            var statisticId = _storageProvider.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
            var time = GetTimeAsync().Result;

            var entity = new CameraActionsEntity
            {
                StatisticId = statisticId,
                CameraTurnOnTime = time
            };

            await _storageProvider.AddCameraActionTurnOnTimeAsync(entity);
        }

        public async Task RecordTurnOffCamera(SocketUser user, SocketVoiceState newVoiceState)
        {
            Console.WriteLine($"{user.Username} turn off the camera in {newVoiceState.VoiceChannel.Name}");
            var userId = await _storageProvider.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);
            var channelId = await _storageProvider.GetChannelIdByAlternateId(newVoiceState.VoiceChannel.Id);
            var statisticId = _storageProvider.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
            var time = GetTimeAsync().Result;

            var entity = new CameraActionsEntity
            {
                StatisticId = statisticId,
                CameraTurnOffTime = time
            };

            await _storageProvider.AddendumCameraActionOperatingTime(entity);
        }

        public async Task RecordTurnOnStream(SocketUser user, SocketVoiceState newVoiceState)
        {
            Console.WriteLine($"{user.Username} turn on the stream in {newVoiceState.VoiceChannel.Name}");
            var userId = await _storageProvider.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);
            var channelId = await _storageProvider.GetChannelIdByAlternateId(newVoiceState.VoiceChannel.Id);
            var statisticId = _storageProvider.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
            var time = GetTimeAsync().Result;

            var entity = new StreamActionsEntity
            {
                StatisticId = statisticId,
                StreamTurnOnTime = time
            };
            await _storageProvider.AddStreamActionTurnOnTimeAsync(entity);
        }

        public async Task RecordTurnOffStream(SocketUser user, SocketVoiceState newVoiceState)
        {
            Console.WriteLine($"{user.Username} turn off the stream in {newVoiceState.VoiceChannel.Name}");
            var userId = await _storageProvider.GetUserIdByAlternateId(alternateId: user.Id, guildId: newVoiceState.VoiceChannel.Guild.Id);
            var channelId = await _storageProvider.GetChannelIdByAlternateId(newVoiceState.VoiceChannel.Id);
            var statisticId = _storageProvider.GetStatisticIdByUserIdAndChannelId(userId, channelId).Result;
            var time = GetTimeAsync().Result;

            var entity = new StreamActionsEntity
            {
                StatisticId = statisticId,
                StreamTurnOffTime = time
            };
            await _storageProvider.AddendumStreamActionOperatingTime(entity);
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
    }
}
