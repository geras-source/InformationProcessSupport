namespace InformationProcessSupport.Core.ScheduleParser
{
    public class Schedule
    {
        public string SubjectName { get; set; }
        public TimeSpan StartTimeTheSubject { get; set; }
        public TimeSpan EndTimeTheSubject { get; set; }
        public string? DayOfTheWeek { get; set; }
        public string? GroupName { get; set; }
        public string? Lecturer { get; set; }
    }
}
