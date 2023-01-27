namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.MicrophoneActions
{
    public interface IMicrophoneActionsRepository
    {
        Task AddMicrophoneTurnOffTimeAsync(MicrophoneActionsEntity microphoneActionsEntity);
        Task AddendumAMicrophoneOperatingTime(MicrophoneActionsEntity microphoneActionsEntity);
    }
}