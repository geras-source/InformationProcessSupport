using InformationProcessSupport.Web.Dtos;

namespace InformationProcessSupport.Web.Services.Contracts
{
    public interface IStatisticServices
    {
        Task<IEnumerable<StatisticDto.StatisticByGroup>> GetStatisticByGroupCollectionAsync();
        Task<IEnumerable<StatisticDto.StatisticByUser>> GetStatisticByUserAsync(string userName);
    }
}
