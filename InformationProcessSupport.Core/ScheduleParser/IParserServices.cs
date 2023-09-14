namespace InformationProcessSupport.Core.ScheduleParser
{
    public interface IParserServices
    {
        Task ParseScheduleCollection(ICollection<Schedule> scheduleCollection);
    }
}
