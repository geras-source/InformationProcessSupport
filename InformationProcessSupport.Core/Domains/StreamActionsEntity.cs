namespace InformationProcessSupport.Core.Domains
{
    public class StreamActionsEntity
    {
        public int StreamActionsEntityId { get; set; }
        public TimeSpan? StreamOperationTime { get; set; }
        public DateTime? StreamTurnOnTime { get; set; }
        public DateTime? StreamTurnOffTime { get; set; }
        public int StatisticId { get; set; }
    }
}
