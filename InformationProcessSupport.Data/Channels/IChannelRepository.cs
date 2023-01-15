namespace InformationProcessSupport.Data.Channels
{
    public interface IChannelRepository
    {
        Task AddAsync(ChannelEntity channel);
        Task AddCollectionAsync(List<ChannelEntity> channels);
        Task UpdateAsync(ChannelEntity channel);
        Task DeleteAsync(ulong id);
        Task<bool> ExistsAsync(ulong id, string guildName);
        Task<ulong> GetChannelIdBuAlternateId(ulong alternateId);
    }
}