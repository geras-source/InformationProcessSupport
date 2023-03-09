namespace InformationProcessSupport.Core.TimeOfActionsInTheChannel.SelfDeafenedActions
{
    public interface ISelfDeafenedActionsRepository
    {
        Task AddSelfDeafenedTurnOffTimeAsync(SelfDeafenedActionsEntity selfDeafenedActionsEntity);
        Task AddendumASelfDeafenedOperatingTime(SelfDeafenedActionsEntity selfDeafenedActionsEntity);
    }
}