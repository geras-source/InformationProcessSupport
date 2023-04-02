namespace InformationProcessSupport.Core.StatisticsCollector.Extensions
{
    internal class GeneratedStatistics
    {
        //----------------------------------------------------------------------------//
        //StatisticEntity
        //----------------------------------------------------------------------------//
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan ConnectionTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EntryTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ExitTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Attendance { get; set; }
        public int EntryCount { get; set; }
        public int ExitCount { get; set; }
        //----------------------------------------------------------------------------//
        //UserEntity
        //----------------------------------------------------------------------------//
        /// <summary>
        /// 
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Nickname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Roles { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? GuildName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? GroupId { get; set; }
        //----------------------------------------------------------------------------//
        //GroupEntity
        //----------------------------------------------------------------------------//
        /// <summary>
        /// 
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //----------------------------------------------------------------------------//
        //ChannelEntity
        //----------------------------------------------------------------------------//
        public string ChannelName { get; set; }
        //----------------------------------------------------------------------------//
        //CameraActionEntity
        //----------------------------------------------------------------------------//
        public TimeSpan? CameraOperationTime { get; set; }
        public DateTime? CameraTurnOnTime { get; set; }
        public DateTime? CameraTurnOffTime { get; set; }
        //----------------------------------------------------------------------------//
        //MicrophoneEntity
        //----------------------------------------------------------------------------//
        public TimeSpan? MicrophoneOperatingTime { get; set; }
        public DateTime MicrophoneTurnOnTime { get; set; }
        public DateTime? MicrophoneTurnOffTime { get; set; }
        //----------------------------------------------------------------------------//
        //SelfDefenedEntity
        //----------------------------------------------------------------------------//
        public TimeSpan? SelfDeafenedOperationTime { get; set; }
        public DateTime SelfDeafenedTurnOnTime { get; set; }
        public DateTime? SelfDeafenedTurnOffTime { get; set; }
        //----------------------------------------------------------------------------//
        //StreamEntity
        //----------------------------------------------------------------------------//
        public TimeSpan? StreamOperationTime { get; set; }
        public DateTime? StreamTurnOnTime { get; set; }
        public DateTime? StreamTurnOffTime { get; set; }
    }
}