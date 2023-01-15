using InformationProcessSupport.Data.TimeOfActionsInTheChannel.MicrophoneActions;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.Statistics
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly ApplicationContext _context;
        public StatisticRepository(ApplicationContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statisticEntity"></param>
        /// <returns></returns>
        public async Task AddAsync(StatisticEntity statisticEntity)
        {
            var entity = new StatisticEntity
            {
                UserId = statisticEntity.UserId,
                ChannelId = statisticEntity.ChannelId,
                EntryTime = statisticEntity.EntryTime
            };

            await _context.StatisticEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetStatisticIdByUserIdAndChannelId(ulong userId, ulong channelId)
        {
            var entity = await _context.StatisticEntities
                .FirstOrDefaultAsync(it => it.ExitTime == null && it.UserId == userId && it.ChannelId == channelId);

            return entity.StatisticId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statisticEntity"></param>
        /// <returns></returns>
        public async Task UpdateConnectionTimeAsync(StatisticEntity statisticEntity)
        {
            var entity = await _context.StatisticEntities
                .FirstOrDefaultAsync(it => it.ExitTime == null && it.UserId == statisticEntity.UserId && it.ChannelId == statisticEntity.ChannelId);
            
            if (entity != null)
            {
                TimeSpan timeSpan = statisticEntity.ExitTime.Value - entity.EntryTime;
                entity.ExitTime = statisticEntity.ExitTime;
                entity.ConnectionTime = timeSpan;
                await _context.SaveChangesAsync();
            }
        }

    }
}