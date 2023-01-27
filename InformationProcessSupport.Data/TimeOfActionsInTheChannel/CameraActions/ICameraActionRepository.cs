using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.CameraActions
{
    public interface ICameraActionRepository
    {
        Task AddCameraActionTurnOnTimeAsync(CameraActionsEntity cameraActionsEntity);
        Task AddendumCameraActionOperatingTime(CameraActionsEntity cameraActionsEntity);
    }
}
