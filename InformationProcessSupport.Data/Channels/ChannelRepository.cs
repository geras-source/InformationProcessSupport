using InformationProcessSupport.Core.Channels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;

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
            var entity = new ChannelModel
            {
                AlternateKey = channel.AlternateKey,
                Name = channel.Name,
                CategoryType = channel.CategoryType,
                GuildId = channel.GuildId,
                GuildName= channel.GuildName
            };
            await _context.ChannelEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddCollectionAsync(List<ChannelEntity> channels)
        {
            var entities = channels.Select(x => new ChannelModel
            {
                AlternateKey = x.AlternateKey,
                Name = x.Name,
                CategoryType = x.CategoryType,
                GuildId = x.GuildId,
                GuildName = x.GuildName
            }).ToList();
            await _context.ChannelEntities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ChannelEntities.SingleAsync(x => x.ChannelId == id);
            if(entity != null)
            {
                _context.ChannelEntities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(ulong id, ulong guildId)
        {
            if (await _context.ChannelEntities.AnyAsync(x => x.AlternateKey == id && x.GuildId == guildId))
            {
                return true;
            }
            return false;
        }

        public async Task<int> GetChannelIdByAlternateId(ulong alternateId)
        {
            var channelId = await _context.ChannelEntities.SingleAsync(x => x.AlternateKey == alternateId);

            return channelId.ChannelId;
        }

        public async Task<int> GetChannelIdByName(string channelName)
        {
            var channelId = await _context.ChannelEntities.SingleAsync(x => x.Name == channelName && x.CategoryType == "Voice");

            return channelId.ChannelId;
        }

        public async Task UpdateAsync(ChannelEntity channel)
        {
            var entity = await _context.ChannelEntities.SingleAsync(x => x.ChannelId == channel.ChannelId && x.GuildId == channel.GuildId);

            if (entity != null)
            {
                entity.Name = channel.Name;
                entity.CategoryType = channel.CategoryType;
                await _context.SaveChangesAsync();
            }
        }
    }
}
