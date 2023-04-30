namespace InformationProcessSupport.Core.Domains
{
    public class ScheduleEntity
    {
        /// <summary>
        /// Первичный ключ в базе данных
        /// </summary>
        public int ScheduleId { get; set; }
        /// <summary>
        /// Название предмета
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// Время начала предмета
        /// </summary>
        public TimeSpan StartTimeTheSubject { get; set; }
        /// <summary>
        /// Время окончания предмета
        /// </summary>
        public TimeSpan EndTimeTheSubject { get; set; }
        /// <summary>
        /// День недели
        /// </summary>
        public DayOfTheWeek DayOfTheWeek { get; set; } = DayOfTheWeek.Monday;
        /// <summary>
        /// Ссылка на группу
        /// </summary>
        public int? GroupId { get; set; }
        /// <summary>
        /// Ссылка на канал в дискорде
        /// </summary>
        public int? ChannelId { get; set; }
    }

    public enum DayOfTheWeek
    {
        Monday = 0,
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4,
        Saturday = 5,
    }
}