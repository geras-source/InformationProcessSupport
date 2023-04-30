namespace InformationProcessSupport.Core.Domains
{
    public class CameraActionsEntity
    {
        public int CameraActionsId { get; set; }
        public TimeSpan? CameraOperationTime { get; set; }
        public DateTime? CameraTurnOnTime { get; set; }
        public DateTime? CameraTurnOffTime { get; set; }
        public int StatisticId { get; set; }
    }
}