using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.Channels
{
    public class ChannelRepository : IChannelRepository
    {
        private readonly ApplicationContext _context;
        public ChannelRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ChannelEntity channel)
        {
            var entity = new ChannelEntity
            {
                AlternateKey = channel.AlternateKey,
                Name = channel.Name,
                CategoryType = channel.CategoryType,
                GuildId = channel.GuildId,
                GuildName= channel.GuildName
            };
            await _context.ChannelsEntity.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddCollectionAsync(List<ChannelEntity> channels)
        {
            await _context.ChannelsEntity.AddRangeAsync(channels);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ChannelsEntity.SingleAsync(x => x.ChannelId == id);
            if(entity != null)
            {
                _context.ChannelsEntity.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(ulong id, ulong guildId)
        {
            if (await _context.ChannelsEntity.AnyAsync(x => x.AlternateKey == id && x.GuildId == guildId))
            {
                return true;
            }
            return false;
        }

        public async Task<int> GetChannelIdBuAlternateId(ulong alternateId)
        {
            var channelId = await _context.ChannelsEntity.SingleAsync(x => x.AlternateKey == alternateId);

            return channelId.ChannelId;
        }

        public async Task UpdateAsync(ChannelEntity channel)
        {
            var entity = await _context.ChannelsEntity.SingleAsync(x => x.ChannelId == channel.ChannelId && x.GuildId == channel.GuildId);

            if (entity != null)
            {
                entity.Name = channel.Name;
                entity.CategoryType = channel.CategoryType;
                await _context.SaveChangesAsync();
            }
        }
    }
}
