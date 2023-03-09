using InformationProcessSupport.Core.TimeOfActionsInTheChannel.MicrophoneActions;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.MicrophoneActions
{
    public class MicrophoneActionsRepository : IMicrophoneActionsRepository
    {
        private readonly ApplicationContext _context;
        public MicrophoneActionsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddMicrophoneTurnOffTimeAsync(MicrophoneActionsEntity microphoneActionsEntity)
        {
            var entity = new MicrophoneActionsModel
            {
                StatistisId = microphoneActionsEntity.StatistisId,
                MicrophoneTurnOffTime = microphoneActionsEntity.MicrophoneTurnOffTime
            };

            await _context.MicrophoneActionEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddendumAMicrophoneOperatingTime(MicrophoneActionsEntity microphoneActionsEntity)
        {
            var entity = await _context.MicrophoneActionEntities
                .FirstOrDefaultAsync(it => it.StatistisId == microphoneActionsEntity.StatistisId && it.MicrophoneOperatingTime == null);
            
            if (entity != null)
            {
                TimeSpan timeSpan = microphoneActionsEntity.MicrophoneTurnOnTime - entity.MicrophoneTurnOffTime.Value;
                entity.MicrophoneTurnOnTime = microphoneActionsEntity.MicrophoneTurnOnTime;
                entity.MicrophoneOperatingTime = timeSpan;
                await _context.SaveChangesAsync();
            }
        }
    }
}