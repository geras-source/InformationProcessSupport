namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel.StreamActions
{
    public interface IStreamActionsRepository
    {
        Task AddStreamActionTurnOnTimeAsync(StreamActionsEntity streamActionsEntity);
        Task AddendumStreamActionOperatingTime(StreamActionsEntity streamActionsEntity);
    }
}