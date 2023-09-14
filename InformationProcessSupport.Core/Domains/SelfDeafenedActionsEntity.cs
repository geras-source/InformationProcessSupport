namespace InformationProcessSupport.Core.Domains
{
    public class SelfDeafenedActionsEntity
    {
        public int SelfDeafenedActionsId { get; set; }
        public TimeSpan? SelfDeafenedOperationTime { get; set; }
        public DateTime SelfDeafenedTurnOnTime { get; set; }
        public DateTime? SelfDeafenedTurnOffTime { get; set; }
        public int StatisticId { get; set; }

    }
}