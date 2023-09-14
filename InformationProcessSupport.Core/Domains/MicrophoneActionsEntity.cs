namespace InformationProcessSupport.Core.Domains
{
    public class MicrophoneActionsEntity
    {
        public int MicrophoneTimeEntityId { get; set; }
        public TimeSpan? MicrophoneOperatingTime { get; set; }
        public DateTime MicrophoneTurnOnTime { get; set; }
        public DateTime? MicrophoneTurnOffTime { get; set; }
        public int StatisticId { get; set; }

    }
}