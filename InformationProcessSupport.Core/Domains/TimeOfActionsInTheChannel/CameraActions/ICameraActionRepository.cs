namespace InformationProcessSupport.Core.TimeOfActionsInTheChannel.CameraActions
{
    public interface ICameraActionRepository
    {
        Task AddCameraActionTurnOnTimeAsync(CameraActionsEntity cameraActionsEntity);
        Task AddendumCameraActionOperatingTime(CameraActionsEntity cameraActionsEntity);
    }
}
