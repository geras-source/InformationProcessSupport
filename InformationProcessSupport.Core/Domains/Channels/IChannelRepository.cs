namespace InformationProcessSupport.Core.Channels
{
    public interface IChannelRepository
    {
        Task AddAsync(ChannelEntity channel);
        Task AddCollectionAsync(List<ChannelEntity> channels);
        Task UpdateAsync(ChannelEntity channel);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(ulong id, ulong guildId);
        Task<int> GetChannelIdByAlternateId(ulong alternateId);
        Task<int> GetChannelIdByName(string channelName);
    }
}