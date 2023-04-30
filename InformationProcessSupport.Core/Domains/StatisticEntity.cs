using System.ComponentModel.DataAnnotations.Schema;

namespace InformationProcessSupport.Core.Domains
{
    [Table("Statistics")]
    public class StatisticEntity
    {
        public int StatisticId { get; set; }
        public TimeSpan ConnectionTime { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public string? Attendance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public int? SheduleId { get; set; }
    }
}