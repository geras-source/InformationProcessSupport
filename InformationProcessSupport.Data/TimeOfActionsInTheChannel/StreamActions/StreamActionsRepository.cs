using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.StreamActions
{
    public class StreamActionsRepository : IStreamActionsRepository
    {
        private readonly ApplicationContext _context;
        public StreamActionsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddStreamActionTurnOnTimeAsync(StreamActionsEntity streamActionsEntity)
        {
            var entity = new StreamActionsEntity
            {
                StatistisId = streamActionsEntity.StatistisId,
                StreamTurnOnTime = streamActionsEntity.StreamTurnOnTime
            };

            await _context.StreamActionsEntity.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddendumStreamActionOperatingTime(StreamActionsEntity streamActionsEntity)
        {
            var entity = await _context.StreamActionsEntity
               .FirstOrDefaultAsync(it => it.StatistisId == streamActionsEntity.StatistisId && it.StreamOperationTime == null);

            if (entity != null)
            {
                TimeSpan timeSpan = streamActionsEntity.StreamTurnOffTime.Value - entity.StreamTurnOnTime.Value;
                entity.StreamTurnOffTime = streamActionsEntity.StreamTurnOffTime;
                entity.StreamOperationTime = timeSpan;
                await _context.SaveChangesAsync();
            }
        }
    }
}