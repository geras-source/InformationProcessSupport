using InformationProcessSupport.Core.TimeOfActionsInTheChannel.SelfDeafenedActions;
using Microsoft.EntityFrameworkCore;


namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.SelfDeafenedActions
{
    public class SelfDeafenedActionsRepository : ISelfDeafenedActionsRepository
    {
        private readonly ApplicationContext _context;
        public SelfDeafenedActionsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddSelfDeafenedTurnOffTimeAsync(SelfDeafenedActionsEntity selfDeafenedActionsEntity)
        {
            var entity = new SelfDeafenedActionsModel
            {
                StatistisId = selfDeafenedActionsEntity.StatistisId,
                SelfDeafenedTurnOffTime = selfDeafenedActionsEntity.SelfDeafenedTurnOffTime
            };

            await _context.SelfDeafenedActionsEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddendumASelfDeafenedOperatingTime(SelfDeafenedActionsEntity selfDeafenedActionsEntity)
        {
            var entity = await _context.SelfDeafenedActionsEntities
                .FirstOrDefaultAsync(it => it.StatistisId == selfDeafenedActionsEntity.StatistisId && it.SelfDeafenedOperationTime == null);

            if (entity != null)
            {
                TimeSpan timeSpan = selfDeafenedActionsEntity.SelfDeafenedTurnOnTime - entity.SelfDeafenedTurnOffTime.Value;
                entity.SelfDeafenedTurnOnTime = selfDeafenedActionsEntity.SelfDeafenedTurnOnTime;
                entity.SelfDeafenedOperationTime = timeSpan;
                await _context.SaveChangesAsync();
            }
        }
       
    }
}
