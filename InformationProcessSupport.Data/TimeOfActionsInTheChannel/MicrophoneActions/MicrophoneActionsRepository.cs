using InformationProcessSupport.Data.Statistics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var entity = new MicrophoneActionsEntity
            {
                StatistisId = microphoneActionsEntity.StatistisId,
                MicrophoneTurnOffTime = microphoneActionsEntity.MicrophoneTurnOffTime
            };

            await _context.MicrophoneActionsEntity.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddendumAMicrophoneOperatingTime(MicrophoneActionsEntity microphoneActionsEntity)
        {
            var entity = await _context.MicrophoneActionsEntity
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
