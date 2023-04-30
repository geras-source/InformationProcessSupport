using System.Diagnostics.CodeAnalysis;
using InformationProcessSupport.Core.Domains;
using InformationProcessSupport.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data
{
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public class StorageProvider : IStorageProvider
    {
        private readonly ApplicationContext _context;

        public StorageProvider(ApplicationContext context)
        {
            _context = context;
        }
        /// --------------------------------------------------------------------
        ///               Методы работы с таблицей User
        /// --------------------------------------------------------------------
        
        public async Task AddUserAsync(UserEntity userEntity)
        {
            var entity = new UserModel
            {
                AlternateKey = userEntity.AlternateKey,
                Name = userEntity.Name,
                Nickname = userEntity.Nickname,
                Roles = userEntity.Roles,
                GuildId = userEntity.GuildId,
                GuildName = userEntity.GuildName
            };
            await _context.UserEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsUserExistsAsync(ulong id, ulong guildId)
        {
            return await _context.UserEntities.AsNoTracking().AnyAsync(x => x.AlternateKey == id && x.GuildId == guildId);
        }
        public async Task<int> GetUserIdByAlternateId(ulong alternateId, ulong guildId)
        {
            var userId = await _context.UserEntities.AsNoTracking().FirstOrDefaultAsync(x => x.AlternateKey == alternateId && x.GuildId == guildId);

            return userId?.UserId ?? default;
        }
        public async Task DeleteUserAsync(int id)
        {
            var entity = await _context.UserEntities.FirstOrDefaultAsync(x => x.UserId == id);

            if (entity != null)
            {
                _context.UserEntities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateUserAsync(UserEntity userEntity)
        {
            var entity = await _context.UserEntities.FirstOrDefaultAsync(x => x.UserId == userEntity.UserId);
            if (entity != null)
            {
                entity.Name = userEntity.Name;
                entity.Nickname = userEntity.Nickname;
                entity.Roles = userEntity.Roles;
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddUserCollectionAsync(List<UserEntity> userEntities)
        {
            var entities = userEntities.Select(x => new UserModel
            {
                AlternateKey = x.AlternateKey,
                Name = x.Name,
                Nickname = x.Nickname,
                Roles = x.Roles,
                GuildId = x.GuildId,
                GuildName = x.GuildName,
                GroupId = x.GroupId
            });
            await _context.UserEntities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<UserEntity>> GetUserCollectionAsync()
        {
            var entities = await _context.UserEntities.Select(it => new UserEntity
            {
                UserId = it.UserId,
                Name = it.Name,
                Nickname = it.Nickname,
                GroupId = it.GroupId
            }).ToListAsync();
            return entities;
        }
        /// --------------------------------------------------------------------
        ///               Методы работы с таблицей Channel
        /// --------------------------------------------------------------------

        public async Task AddChannelAsync(ChannelEntity channel)
        {
            var entity = new ChannelModel
            {
                AlternateKey = channel.AlternateKey,
                Name = channel.Name,
                CategoryType = channel.CategoryType,
                GuildId = channel.GuildId,
                GuildName = channel.GuildName
            };
            await _context.ChannelEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddChannelCollectionAsync(List<ChannelEntity> channels)
        {
            var entities = channels.Select(x => new ChannelModel
            {
                AlternateKey = x.AlternateKey,
                Name = x.Name,
                CategoryType = x.CategoryType,
                GuildId = x.GuildId,
                GuildName = x.GuildName
            }).ToList();
            await _context.ChannelEntities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChannelAsync(int id)
        {
            var entity = await _context.ChannelEntities.FirstOrDefaultAsync(x => x.ChannelId == id);
            if (entity != null)
            {
                _context.ChannelEntities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsChannelExistsAsync(ulong id, ulong guildId)
        {
            return await _context.ChannelEntities.AnyAsync(x => x.AlternateKey == id && x.GuildId == guildId);
        }

        public async Task<int> GetChannelIdByAlternateId(ulong alternateId)
        {
            var entity = await _context.ChannelEntities.FirstOrDefaultAsync(x => x.AlternateKey == alternateId);

            return entity?.ChannelId ?? default;
        }

        public async Task<int> GetChannelIdByName(string channelName)
        {
            var entity = await _context.ChannelEntities.AsNoTracking().FirstOrDefaultAsync(x => x.Name == channelName && x.CategoryType == "Voice");

            if (entity == null)
            {
                throw new ArgumentException("Sequence contains no element", channelName);
            }

            return entity.ChannelId;
        }

        public async Task<ICollection<ChannelEntity>> GetChannelCollectionAsync()
        {
            var entities = await _context.ChannelEntities.AsNoTracking().Select(it => new ChannelEntity
            {
                ChannelId = it.ChannelId,
                AlternateKey = it.AlternateKey,
                CategoryType = it.CategoryType,
                GuildId = it.GuildId,
                GuildName = it.GuildName,
                Name = it.Name
            }).ToListAsync();

            return entities;
        }

        public async Task UpdateChannelAsync(ChannelEntity channel)
        {
            var entity = await _context.ChannelEntities.FirstOrDefaultAsync(x => x.ChannelId == channel.ChannelId && x.GuildId == channel.GuildId);

            if (entity != null)
            {
                entity.Name = channel.Name;
                entity.CategoryType = channel.CategoryType;
                await _context.SaveChangesAsync();
            }
        }

        /// --------------------------------------------------------------------
        ///               Методы работы с таблицей Groups
        /// --------------------------------------------------------------------
        public async Task AddGroupAsync(GroupEntity groupEntity)
        {
            var entity = new GroupModel
            {
                GroupName = groupEntity.GroupName,
                AlternateKey = groupEntity.AlternateKey,
                GuildId = groupEntity.GuildId,
                GuildName = groupEntity.GuildName
            };
            await _context.GroupEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddGroupCollectionAsync(List<GroupEntity> groupEntities)
        {
            var entities = groupEntities.Select(x => new GroupModel
            {
                GroupName = x.GroupName,
                AlternateKey = x.AlternateKey,
                GuildId = x.GuildId,
                GuildName = x.GuildName
            }).ToList();
            await _context.GroupEntities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGroupAsync(int groupId)
        {
            var entity = await _context.GroupEntities.FirstOrDefaultAsync(x => x.GroupId == groupId);

            if (entity != null)
            {
                _context.GroupEntities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<GroupEntity>> GetGroupCollectionAsync()
        {
            var entities = await _context.GroupEntities.Select(it => new GroupEntity
            {
                GroupId = it.GroupId,
                GroupName = it.GroupName
            }).ToListAsync();

            return entities;
        }

        public async Task<int> GetGroupIdByAlternateId(ulong alternateId, ulong guildId)
        {
            var entity = await _context.GroupEntities.FirstOrDefaultAsync(x => x.AlternateKey == alternateId && x.GuildId == guildId);

            return entity?.GroupId ?? default;
        }

        public async Task<int> GetGroupIdByName(string groupName)
        {
            var entity = await _context.GroupEntities.FirstOrDefaultAsync(x => x.GroupName == groupName);

            if (entity == null)
            {
                throw new ArgumentException("Sequence contains no element", groupName);
            }

            return entity.GroupId;
        }

        public async Task UpdateGroupAsync(GroupEntity groupEntity)
        {
            var entity = await _context.GroupEntities.FirstOrDefaultAsync(x => x.GroupId == groupEntity.GroupId);

            if (entity != null)
            {
                entity.GroupName = groupEntity.GroupName;
                await _context.SaveChangesAsync();
            }
        }

        /// --------------------------------------------------------------------
        ///               Методы работы с таблицей Schedule
        /// --------------------------------------------------------------------

        public async Task AddScheduleCollectionAsync(ICollection<ScheduleEntity> scheduleCollection)
        {
            var entities = scheduleCollection.Select(x => new ScheduleModel
            {
                SubjectName = x.SubjectName,
                StartTimeTheSubject = x.StartTimeTheSubject,
                EndTimeTheSubject = x.EndTimeTheSubject,
                DayOfTheWeek = x.DayOfTheWeek,
                GroupId = x.GroupId,
                ChannelId = x.ChannelId
            }).ToList();
            await _context.ScheduleEntities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task AddScheduleForOneSubject(ScheduleEntity scheduleEntity)
        {
            var entity = new ScheduleModel
            {
                SubjectName = scheduleEntity.SubjectName,
                StartTimeTheSubject = scheduleEntity.StartTimeTheSubject,
                EndTimeTheSubject = scheduleEntity.EndTimeTheSubject,
                ChannelId = scheduleEntity.ChannelId,
                DayOfTheWeek = scheduleEntity.DayOfTheWeek,
                GroupId = scheduleEntity.GroupId
            };
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ScheduleEntity> GetScheduleByChannelIdAsync(int channelId, TimeOnly timeNow)
        {
            var model = await _context.ScheduleEntities.Where(x =>
                    timeNow >= timeNow/*TimeOnly.FromTimeSpan(x.StartTimeTheSubject)*/ &&
                    timeNow <= timeNow/*TimeOnly.FromTimeSpan(x.EndTimeTheSubject)*/)
                .FirstOrDefaultAsync(x => x.ChannelId == channelId); //TODO: переделать на TimeOnly

            if (model == null) return null;
            var entity = new ScheduleEntity
            {
                ScheduleId = model.ScheduleId,
                DayOfTheWeek = model.DayOfTheWeek,
                StartTimeTheSubject = model.StartTimeTheSubject,
                EndTimeTheSubject = model.EndTimeTheSubject,
                SubjectName = model.SubjectName
            };

            return entity;
        }

        public async Task<IEnumerable<ScheduleEntity>> GetScheduleCollectionAsync()
        {
            var entities = await _context.ScheduleEntities.AsNoTracking().Select(it => new ScheduleEntity()
            {
                SubjectName = it.SubjectName,
                StartTimeTheSubject = it.StartTimeTheSubject,
                EndTimeTheSubject = it.EndTimeTheSubject,
                ScheduleId = it.ScheduleId,
                DayOfTheWeek = it.DayOfTheWeek
            }).ToListAsync();
            return entities;
        }

        /// --------------------------------------------------------------------
        ///               Методы работы с таблицей Statistic
        /// --------------------------------------------------------------------
        public async Task AddStatisticAsync(StatisticEntity statisticEntity)
        {
            var entity = new StatisticModel
            {
                UserId = statisticEntity.UserId,
                ChannelId = statisticEntity.ChannelId,
                EntryTime = statisticEntity.EntryTime,
                Attendance = statisticEntity.Attendance,
                ScheduleId = statisticEntity.SheduleId
            };

            await _context.StatisticEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<StatisticEntity>> GetStatisticCollectionsAsync()
        {
            var entities = await _context.StatisticEntities.AsNoTracking().Select(it => new StatisticEntity
            {
                StatisticId = it.StatisticId,
                ConnectionTime = it.ConnectionTime,
                EntryTime = it.EntryTime,
                ExitTime = it.ExitTime,
                Attendance = it.Attendance,
                UserId = it.UserId,
                ChannelId = it.ChannelId,
                SheduleId = it.ScheduleId
            }).ToListAsync();

            return entities;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<ICollection<StatisticEntity>> GetStatisticCollectionsByDateAsync(DateTime date)
        {
            var entity = await _context.StatisticEntities
                .AsNoTracking()
                .Where(x => x.EntryTime.Date == date.Date)
                .Select(it => new StatisticEntity
            {
                StatisticId = it.StatisticId,
                ConnectionTime = it.ConnectionTime,
                EntryTime = it.EntryTime,
                ExitTime = it.ExitTime,
                Attendance = it.Attendance,
                UserId = it.UserId,
                ChannelId = it.ChannelId,
                SheduleId = it.ScheduleId
            }).ToListAsync();
            return entity;
        }

        public async Task<int> GetStatisticIdByUserIdAndChannelId(int userId, int channelId)
        {
            var entity = await _context.StatisticEntities.
                AsNoTracking().
                FirstOrDefaultAsync(it => it.ExitTime == null && it.UserId == userId && it.ChannelId == channelId);

            return entity?.StatisticId ?? default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statisticEntity"></param>
        /// <returns></returns>
        public async Task UpdateConnectionTimeInStatisticAsync(StatisticEntity statisticEntity)
        {
            var entity = await _context.StatisticEntities
                .FirstOrDefaultAsync(it => it.ExitTime == null && it.UserId == statisticEntity.UserId && it.ChannelId == statisticEntity.ChannelId);

            if (entity != null)
            {
                TimeSpan timeSpan = statisticEntity.ExitTime.Value - entity.EntryTime;
                entity.ExitTime = statisticEntity.ExitTime;
                entity.ConnectionTime = timeSpan;
                await _context.SaveChangesAsync();
            }
        }

        /// --------------------------------------------------------------------
        ///               Методы работы с таблицей Camera
        /// --------------------------------------------------------------------
        public async Task AddCameraActionTurnOnTimeAsync(CameraActionsEntity cameraActionsEntity)
        {
            var entity = new CameraActionsModel
            {
                StatisticId = cameraActionsEntity.StatisticId,
                CameraTurnOnTime = cameraActionsEntity.CameraTurnOnTime
            };

            await _context.CameraActionEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddendumCameraActionOperatingTime(CameraActionsEntity cameraActionsEntity)
        {
            var entity = await _context.CameraActionEntities
                .FirstOrDefaultAsync(it => it.StatisticId == cameraActionsEntity.StatisticId && it.CameraOperationTime == null);

            if (entity != null)
            {
                TimeSpan timeSpan = cameraActionsEntity.CameraTurnOffTime.Value - entity.CameraTurnOnTime.Value;
                entity.CameraTurnOffTime = cameraActionsEntity.CameraTurnOffTime;
                entity.CameraOperationTime = timeSpan;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CameraActionsEntity>> GetCameraCollectionAsync()
        {
            var entities = await _context.CameraActionEntities.AsNoTracking().Select(it => new CameraActionsEntity()
            {
                StatisticId = it.StatisticId,
                CameraOperationTime = it.CameraOperationTime,
                CameraTurnOffTime = it.CameraTurnOffTime,
                CameraTurnOnTime = it.CameraTurnOnTime
            }).ToListAsync();
            return entities;
        }
        /// --------------------------------------------------------------------
        ///               Методы работы с таблицей Microphone
        /// --------------------------------------------------------------------
        public async Task AddMicrophoneTurnOffTimeAsync(MicrophoneActionsEntity microphoneActionsEntity)
        {
            var entity = new MicrophoneActionsModel
            {
                StatisticId = microphoneActionsEntity.StatisticId,
                MicrophoneTurnOffTime = microphoneActionsEntity.MicrophoneTurnOffTime
            };

            await _context.MicrophoneActionEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddendumAMicrophoneOperatingTime(MicrophoneActionsEntity microphoneActionsEntity)
        {
            var entity = await _context.MicrophoneActionEntities
                .FirstOrDefaultAsync(it => it.StatisticId == microphoneActionsEntity.StatisticId && it.MicrophoneOperatingTime == null);

            if (entity != null)
            {
                TimeSpan timeSpan = microphoneActionsEntity.MicrophoneTurnOnTime - entity.MicrophoneTurnOffTime.Value;
                entity.MicrophoneTurnOnTime = microphoneActionsEntity.MicrophoneTurnOnTime;
                entity.MicrophoneOperatingTime = timeSpan;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MicrophoneActionsEntity>> GetMicrophoneCollectionAsync()
        {
            var entities = await _context.MicrophoneActionEntities.AsNoTracking().Select(it => new MicrophoneActionsEntity
            {
                StatisticId = it.StatisticId,
                MicrophoneOperatingTime = it.MicrophoneOperatingTime,
                MicrophoneTurnOffTime = it.MicrophoneTurnOffTime,
                MicrophoneTurnOnTime = it.MicrophoneTurnOnTime
            }).ToListAsync();

            return entities;
        }
        /// --------------------------------------------------------------------
        ///               Методы работы с таблицей SelfDeafened
        /// --------------------------------------------------------------------
        public async Task AddSelfDeafenedTurnOffTimeAsync(SelfDeafenedActionsEntity selfDeafenedActionsEntity)
        {
            var entity = new SelfDeafenedActionsModel
            {
                StatisticId = selfDeafenedActionsEntity.StatisticId,
                SelfDeafenedTurnOffTime = selfDeafenedActionsEntity.SelfDeafenedTurnOffTime
            };

            await _context.SelfDeafenedActionsEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddendumASelfDeafenedOperatingTime(SelfDeafenedActionsEntity selfDeafenedActionsEntity)
        {
            var entity = await _context.SelfDeafenedActionsEntities
                .FirstOrDefaultAsync(it => it.StatisticId == selfDeafenedActionsEntity.StatisticId && it.SelfDeafenedOperationTime == null);

            if (entity != null)
            {
                TimeSpan timeSpan = selfDeafenedActionsEntity.SelfDeafenedTurnOnTime - entity.SelfDeafenedTurnOffTime.Value;
                entity.SelfDeafenedTurnOnTime = selfDeafenedActionsEntity.SelfDeafenedTurnOnTime;
                entity.SelfDeafenedOperationTime = timeSpan;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SelfDeafenedActionsEntity>> GetSelfDeafenedCollectionAsync()
        {
            var entities = await _context.SelfDeafenedActionsEntities.AsNoTracking().Select(it =>
                new SelfDeafenedActionsEntity()
                {
                    StatisticId = it.StatisticId,
                    SelfDeafenedOperationTime = it.SelfDeafenedOperationTime,
                    SelfDeafenedTurnOffTime = it.SelfDeafenedTurnOffTime,
                    SelfDeafenedTurnOnTime = it.SelfDeafenedTurnOnTime
                }).ToListAsync();
            return entities;
        }
        /// --------------------------------------------------------------------
        ///               Методы работы с таблицей Stream
        /// --------------------------------------------------------------------
        public async Task AddStreamActionTurnOnTimeAsync(StreamActionsEntity streamActionsEntity)
        {
            var entity = new StreamActionsModel
            {
                StatisticId = streamActionsEntity.StatisticId,
                StreamTurnOnTime = streamActionsEntity.StreamTurnOnTime
            };

            await _context.StreamActionEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddendumStreamActionOperatingTime(StreamActionsEntity streamActionsEntity)
        {
            var entity = await _context.StreamActionEntities
                .FirstOrDefaultAsync(it => it.StatisticId == streamActionsEntity.StatisticId && it.StreamOperationTime == null);

            if (entity != null)
            {
                TimeSpan timeSpan = streamActionsEntity.StreamTurnOffTime.Value - entity.StreamTurnOnTime.Value;
                entity.StreamTurnOffTime = streamActionsEntity.StreamTurnOffTime;
                entity.StreamOperationTime = timeSpan;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<StreamActionsEntity>> GetStreamCollectionAsync()
        {
            var entities = await _context.StreamActionEntities.AsNoTracking().Select(it => new StreamActionsEntity()
            {
                StatisticId = it.StatisticId,
                StreamOperationTime = it.StreamOperationTime,
                StreamTurnOffTime = it.StreamTurnOffTime,
                StreamTurnOnTime = it.StreamTurnOnTime
            }).ToListAsync();
            return entities;
        }
    }
}
