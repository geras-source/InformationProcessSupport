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
                CategoryType = channel.CategoryType
            };
            await _context.Channels.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddCollectionAsync(List<ChannelEntity> channels)
        {
            await _context.Channels.AddRangeAsync(channels);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(ulong id)
        {
            //var entity = await _context.Users.FirstOrDefaultAsync(it => it.Id == id);

            //if (entity != null)
            //{
            //    _context.Remove(entity);
            //    await _context.SaveChangesAsync();
            //}
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(ulong id, string guildName)
        {
            if (await _context.Channels.AnyAsync(x => x.AlternateKey == id && x.GuildName == guildName))
            {
                return true;
            }
            return false;
        }

        public async Task<ulong> GetChannelIdBuAlternateId(ulong alternateId)
        {
            var channelId = await _context.Channels.SingleAsync(x => x.AlternateKey == alternateId);

            return channelId.ChannelId;
        }

        public Task UpdateAsync(ChannelEntity channel)
        {
            //var entity = await _context.Users.SingleAsync(it => it.Id == user.Id);

            //if (entity != null)
            //{
            //    entity.Login = user.Login;
            //    entity.Email = user.Email;
            //    await _context.SaveChangesAsync();
            //}
            throw new NotImplementedException();
        }
    }
}
