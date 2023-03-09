using InformationProcessSupport.Website.Services;

namespace InformationProcessSupport.Website.Pages
{
    public partial class Counter
    {
        private ScheduleEntity _scheduleEntity = new();
        private string time = "";
        private async Task IncrementCount()
        {
            // HTTP POSt
            HttpService httpService = new HttpService();
            //_scheduleEntity.StartTimeTheSubject = TimeSpan.Parse(time);
            await httpService.PostSchedule(_scheduleEntity);
        }
    }
    public class ScheduleEntity
    {
        public string SubjectName { get; set; }
        public TimeSpan StartTimeTheSubject { get; set; }
        //public TimeSpan EndTimeTheSubject { get; set; }
    }
}
