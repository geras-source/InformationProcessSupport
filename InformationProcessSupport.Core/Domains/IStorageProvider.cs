namespace InformationProcessSupport.Core.Domains
{
    public interface IStorageProvider
    {
        Task AddUserAsync(UserEntity userEntity);
        Task<bool> IsUserExistsAsync(ulong id, ulong guildId);
        Task<int> GetUserIdByAlternateId(ulong alternateId, ulong guildId);
        Task DeleteUserAsync(int id);
        Task UpdateUserAsync(UserEntity userEntity);
        Task AddUserCollectionAsync(List<UserEntity> userEntities);
        Task<ICollection<UserEntity>> GetUserCollectionAsync();
        Task AddChannelAsync(ChannelEntity channel);
        Task AddChannelCollectionAsync(List<ChannelEntity> channels);
        Task DeleteChannelAsync(int id);
        Task<bool> IsChannelExistsAsync(ulong id, ulong guildId);
        Task<int> GetChannelIdByAlternateId(ulong alternateId);
        Task<int> GetChannelIdByName(string channelName);
        Task<ICollection<ChannelEntity>> GetChannelCollectionAsync();
        Task UpdateChannelAsync(ChannelEntity channel);
        Task AddGroupAsync(GroupEntity groupEntity);
        Task AddGroupCollectionAsync(List<GroupEntity> groupEntities);
        Task DeleteGroupAsync(int groupId);
        Task<ICollection<GroupEntity>> GetGroupCollectionAsync();
        Task<int> GetGroupIdByAlternateId(ulong alternateId, ulong guildId);
        Task<int> GetGroupIdByName(string groupName);
        Task UpdateGroupAsync(GroupEntity groupEntity);
        Task AddScheduleCollectionAsync(ICollection<ScheduleEntity> scheduleCollection);
        Task AddScheduleForOneSubject(ScheduleEntity scheduleEntity);
        Task<ScheduleEntity> GetScheduleByChannelIdAsync(int channelId, TimeOnly timeNow);
        Task<IEnumerable<ScheduleEntity>> GetScheduleCollectionAsync();
        Task AddStatisticAsync(StatisticEntity statisticEntity);
        Task<ICollection<StatisticEntity>> GetStatisticCollectionsAsync();
        Task<ICollection<StatisticEntity>> GetStatisticCollectionsByDateAsync(DateTime date);
        Task<int> GetStatisticIdByUserIdAndChannelId(int userId, int channelId);
        Task UpdateConnectionTimeInStatisticAsync(StatisticEntity statisticEntity);
        Task AddCameraActionTurnOnTimeAsync(CameraActionsEntity cameraActionsEntity);
        Task AddendumCameraActionOperatingTime(CameraActionsEntity cameraActionsEntity);
        Task<IEnumerable<CameraActionsEntity>> GetCameraCollectionAsync();
        Task AddMicrophoneTurnOffTimeAsync(MicrophoneActionsEntity microphoneActionsEntity);
        Task AddendumAMicrophoneOperatingTime(MicrophoneActionsEntity microphoneActionsEntity);
        Task<IEnumerable<MicrophoneActionsEntity>> GetMicrophoneCollectionAsync();
        Task AddSelfDeafenedTurnOffTimeAsync(SelfDeafenedActionsEntity selfDeafenedActionsEntity);
        Task AddendumASelfDeafenedOperatingTime(SelfDeafenedActionsEntity selfDeafenedActionsEntity);
        Task<IEnumerable<SelfDeafenedActionsEntity>> GetSelfDeafenedCollectionAsync();
        Task AddStreamActionTurnOnTimeAsync(StreamActionsEntity streamActionsEntity);
        Task AddendumStreamActionOperatingTime(StreamActionsEntity streamActionsEntity);
        Task<IEnumerable<StreamActionsEntity>> GetStreamCollectionAsync();

    }
}
