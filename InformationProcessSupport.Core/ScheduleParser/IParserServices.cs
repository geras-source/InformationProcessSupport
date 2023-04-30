using InformationProcessSupport.Core;
using Microsoft.AspNetCore.Http;

namespace InformationProcessSupport.Core.ScheduleParser
{
    public interface IParserServices
    {
        Task ParseScheduleCollection(ICollection<Schedule> scheduleCollection);
    }
}
