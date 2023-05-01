using InformationProcessSupport.Web.Dtos;

namespace InformationProcessSupport.Web.Services.Contracts
{
    public interface IScheduleServices
    {
        Task AddScheduleCollectionAsync(ICollection<ScheduleDto>  schedules);
    }
}
