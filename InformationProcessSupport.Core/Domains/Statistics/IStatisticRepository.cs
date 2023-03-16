namespace InformationProcessSupport.Core.Statistics
{
    public interface IStatisticRepository
    {
        Task AddAsync(StatisticEntity statisticEntity);
        Task UpdateConnectionTimeAsync(StatisticEntity statisticEntity);
        Task<int> GetStatisticIdByUserIdAndChannelId(int userId, int channelId);
        Task<ICollection<StatisticEntity>> GetStatisticCollectionsAsync();
        Task<ICollection<StatisticEntity>> GetStatisticCollectionsByDateAsync(string date);
    }
}