﻿using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.CameraActions
{
    public class CameraActionRepository : ICameraActionRepository
    {
        private readonly ApplicationContext _context;
        public CameraActionRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddCameraActionTurnOnTimeAsync(CameraActionsEntity cameraActionsEntity)
        {
            var entity = new CameraActionsEntity
            {
                StatistisId = cameraActionsEntity.StatistisId,
                CameraTurnOnTime = cameraActionsEntity.CameraTurnOnTime
            };

            await _context.CameraActionsEntity.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddendumCameraActionOperatingTime(CameraActionsEntity cameraActionsEntity)
        {
            var entity = await _context.CameraActionsEntity
               .FirstOrDefaultAsync(it => it.StatistisId == cameraActionsEntity.StatistisId && it.CameraOperationTime == null);

            if (entity != null)
            {
                TimeSpan timeSpan = cameraActionsEntity.CameraTurnOffTime.Value - entity.CameraTurnOnTime.Value;
                entity.CameraTurnOffTime = cameraActionsEntity.CameraTurnOffTime;
                entity.CameraOperationTime = timeSpan;
                await _context.SaveChangesAsync();
            }
        }
    }
}
