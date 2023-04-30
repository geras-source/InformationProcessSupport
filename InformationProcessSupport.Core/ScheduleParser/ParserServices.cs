using InformationProcessSupport.Core.Domains;

namespace InformationProcessSupport.Core.ScheduleParser
{
    public class ParserServices : IParserServices
    {
        private readonly IStorageProvider _storageProvider;

        public ParserServices(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }
        public async Task ParseScheduleCollection(ICollection<Schedule> scheduleCollection)
        {
            List<ScheduleEntity> scheduleEntities = new();
            foreach (var schedule in scheduleCollection)
            {
                var day = schedule.DayOfTheWeek switch
                {
                    "Monday" => DayOfTheWeek.Monday,
                    "Tuesday" => DayOfTheWeek.Tuesday,
                    "Wednesday" => DayOfTheWeek.Wednesday,
                    "Thursday" => DayOfTheWeek.Thursday,
                    "Friday" => DayOfTheWeek.Friday,
                    "Saturday" => DayOfTheWeek.Saturday,
                    _ => DayOfTheWeek.Monday,
                };
                var channelId = await _storageProvider.GetChannelIdByName(schedule.Lecturer);
                var groupId = await _storageProvider.GetGroupIdByName(schedule.GroupName);
                var entity = new ScheduleEntity
                {
                    SubjectName = schedule.SubjectName,
                    StartTimeTheSubject = schedule.StartTimeTheSubject,
                    EndTimeTheSubject = schedule.EndTimeTheSubject,
                    DayOfTheWeek = day,
                    GroupId = groupId,
                    ChannelId = channelId
                };
                scheduleEntities.Add(entity);
            }
            await _storageProvider.AddScheduleCollectionAsync(scheduleEntities);
        }
    }
}
