namespace InformationProcessSupport.Core.ScheduleOfSubjects
{
    public interface IScheduleRepository
    {
        Task AddScheduleForOneSubject(ScheduleEntity scheduleEntity);
        Task AddScheduleCollectionAsync(ICollection<ScheduleEntity> scheduleCollection);
        Task<ScheduleEntity> GetScheduleByChannelIdAsync(int channelId);
    }
}
