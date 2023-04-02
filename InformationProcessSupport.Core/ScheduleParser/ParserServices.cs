using InformationProcessSupport.Core.Channels;
using InformationProcessSupport.Core.Groups;
using InformationProcessSupport.Core.ScheduleOfSubjects;

namespace InformationProcessSupport.Core.ScheduleParser
{
    public class ParserServices : IParserServices
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IGroupRepository _groupReposity;
        private readonly IChannelRepository _channelRepository;

        public ParserServices(IScheduleRepository scheduleRepository, IGroupRepository groupReposity, IChannelRepository channelRepository)
        {
            _scheduleRepository = scheduleRepository;
            _groupReposity = groupReposity;
            _channelRepository = channelRepository;
        }
        public async Task ParseScheduleCollection(ICollection<Schedule> scheduleCollection)
        {
            DayOfTheWeek day;
            List<ScheduleEntity> scheduleEntities = new();
            foreach (var schedule in scheduleCollection)
            {
                day = schedule.DayOfTheWeek switch
                {
                    "Monday" => DayOfTheWeek.Monday,
                    "Tuesday" => DayOfTheWeek.Tuesday,
                    "Wednesday" => DayOfTheWeek.Wednesday,
                    "Thursday" => DayOfTheWeek.Thursday,
                    "Friday" => DayOfTheWeek.Friday,
                    "Saturday" => DayOfTheWeek.Saturday,
                    _ => DayOfTheWeek.Monday,
                };
                int channelId = await _channelRepository.GetChannelIdByName(schedule.Lecturer);
                int groupId = await _groupReposity.GetGroupIdByName(schedule.GroupName);
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
            await _scheduleRepository.AddScheduleCollectionAsync(scheduleEntities);
        }
    }
}
