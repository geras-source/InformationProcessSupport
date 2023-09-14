namespace InformationProcessSupport.Web.Dtos
{
    public class StatisticDto
    {
        public class StatisticByGroup
        {
            public string GroupName { get; set; }
            public string SubjectName { get; set; }
            public string LectureName { get; set; }
            public double PercentageOfAttendance { get; set; }
            public double PercentageOfMicrophoneActivity { get; set; }
            public double PercentageOfStreamActivity { get; set; }
            public double PercentageOfVideoActivity { get; set; }
            public double PercentageOfSelfDeafenedActivity { get; set; }
        }

        public class StatisticByUser
        {
            public string UserName { get; set; }
            public double PercentageOfAttendance { get; set; }
            public double PercentageOfMicrophoneActivity { get; set; }
            public double PercentageOfStreamActivity { get; set; }
            public double PercentageOfVideoActivity { get; set; }
            public double PercentageOfSelfDeafenedActivity { get; set; }
            public string GroupName { get; set; }
            public string SubjectName { get; set; }

        }

    }
}
