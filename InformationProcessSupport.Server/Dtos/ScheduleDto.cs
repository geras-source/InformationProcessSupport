namespace InformationProcessSupport.Server.Dtos
{
    public class ScheduleDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan StartTimeTheSubject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan EndTimeTheSubject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? DayOfTheWeek { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? GroupName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Lecturer { get; set; }
    }
}
