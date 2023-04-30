using InformationProcessSupport.Core.Domains;

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
        public string SubjectName { get; set; }
        public TimeSpan StartTimeTheSubject { get; set; }
        public TimeSpan EndTimeTheSubject { get; set; }
        public TimeSpan MicrophoneOperatingTime { get; set; }
        public TimeSpan CameraOperatingTime { get; set; }
        public TimeSpan StreamOperatingTime { get; set; }
        public TimeSpan SelfDeafenedOperatingTime { get; set; }
        public List<MicrophoneActionsEntity> MicrophoneActionsEntity { get; set; }
        public List<CameraActionsEntity> CameraActionsEntity { get; set; }
        public List<StreamActionsEntity> StreamActionsEntity { get; set; }
        public List<SelfDeafenedActionsEntity> SelfDeafenedActionsEntities { get; set; }
    }
}