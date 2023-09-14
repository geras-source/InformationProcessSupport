using InformationProcessSupport.Core.StatisticsCollector;
using InformationProcessSupport.Data;
using InformationProcessSupport.Data.Models;
using InformationProcessSupport.Server.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Server.Controllers.Statistics
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticCollectorServices _collectorServices;
        private readonly ApplicationContext _context;

        public StatisticController(IStatisticCollectorServices collectorServices, ApplicationContext context)
        {
            _collectorServices= collectorServices;
            _context = context;
        }
        [HttpGet("[action]/{date}")]
        public async Task<ActionResult> CreateReportByDateAsync(DateTime date)
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"Report ({date.Date}).xlsx";
            var memoryStream = new MemoryStream();
            var result = await _collectorServices.CreateReportByDate(date);
            result.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new FileStreamResult(memoryStream, contentType)
            {
                FileDownloadName = fileName
            };
        }
        [HttpGet("[action]")] //TODO: refactoring
        public async Task<ActionResult<IEnumerable<StatisticDto.StatisticByGroup>>> GetStatisticsByGroupAsync()
        {
            var test = _context.StatisticEntities.AsNoTracking()
                .Include(x => x.ChannelEntity)
                .Include(x => x.UserEntity.GroupEntity)
                .Include(x => x.ScheduleEntity)
                .Include(x => x.MicrophoneActionsEntity)
                .Include(x => x.CameraActionsEntity)
                .Include(x => x.StreamActionsEntity)
                .Include(x => x.SelfDeafenedActionsEntities);

            var temp = test.GroupBy(x => x.UserEntity.GroupEntity!.GroupName);
            var lisst = new List<StatisticDto.StatisticByGroup>();
            foreach (var group in temp)
            {
                var temp2 = group.GroupBy(x => x.ScheduleEntity.SubjectName);

                foreach (var statistic in temp2)
                {
                    var percentageOfAttendance = statistic.Select(
                        x => (x.ConnectionTime.TotalSeconds /
                              x.ScheduleEntity.EndTimeTheSubject.Subtract(x.ScheduleEntity.StartTimeTheSubject)
                                  .TotalSeconds) * 100).Sum();

                    var percentageOfMicrophoneActivity = statistic.Select(
                        x => (GetMicrophoneOperationTime(statistic).TotalSeconds /
                              x.ScheduleEntity.EndTimeTheSubject.Subtract(x.ScheduleEntity.StartTimeTheSubject)
                                  .TotalSeconds) * 100).Sum();
                    var percentageOfStreamActivity = statistic.Select(
                        x => (GetStreamOperationTime(statistic).TotalSeconds /
                              x.ScheduleEntity.EndTimeTheSubject.Subtract(x.ScheduleEntity.StartTimeTheSubject)
                                  .TotalSeconds) * 100).Sum();
                    
                    var percentageOfVideoActivity = statistic.Select(
                        x => (GetCameraOperationTime(statistic).TotalSeconds /
                              x.ScheduleEntity.EndTimeTheSubject.Subtract(x.ScheduleEntity.StartTimeTheSubject)
                                  .TotalSeconds) * 100).Sum();
                    
                    var percentageOfSelfDeafenedActivity = statistic.Select(
                        x => (GetSelfDeafenedOperationTime(statistic).TotalSeconds /
                              x.ScheduleEntity.EndTimeTheSubject.Subtract(x.ScheduleEntity.StartTimeTheSubject)
                                  .TotalSeconds) * 100).Sum();

                    lisst.Add(new StatisticDto.StatisticByGroup
                    {
                        GroupName = group.Key,
                        SubjectName = statistic.Key,
                        PercentageOfAttendance = Math.Round(percentageOfAttendance, 2),
                        PercentageOfMicrophoneActivity = percentageOfMicrophoneActivity,
                        PercentageOfVideoActivity = percentageOfVideoActivity,
                        PercentageOfSelfDeafenedActivity = percentageOfSelfDeafenedActivity,
                        PercentageOfStreamActivity = percentageOfStreamActivity,
                        LectureName = statistic.First().ChannelEntity.Name
                    });
                }

            }

            return Ok(lisst);
        }

        [HttpGet("[action]/{userName}")]
        public async Task<ActionResult<IEnumerable<StatisticDto.StatisticByUser>>> GetStatisticByUserAsync(string userName)
        {
            var test = await _context.StatisticEntities.AsNoTracking()
                .Include(x => x.UserEntity.GroupEntity)
                .Where(x => x.UserEntity.Nickname == userName)
                .Include(x => x.ScheduleEntity)
                .Include(x => x.CameraActionsEntity)
                .Include(x => x.MicrophoneActionsEntity)
                .Include(x => x.StreamActionsEntity)
                .Include(x => x.SelfDeafenedActionsEntities)
                .ToListAsync();

            var group = test.GroupBy(x => x.ScheduleEntity.SubjectName);

            var users = new List<StatisticDto.StatisticByUser>();

            foreach (var item in group)
            {
                var percentageOfAttendance = item.Select(
                    x => (x.ConnectionTime.TotalSeconds /
                          x.ScheduleEntity.EndTimeTheSubject.Subtract(x.ScheduleEntity.StartTimeTheSubject)
                              .TotalSeconds) * 100).Sum();

                var percentageOfMicrophoneActivity = item.Select(
                    x => (GetMicrophoneOperationTime(item).TotalSeconds /
                          x.ScheduleEntity.EndTimeTheSubject.Subtract(x.ScheduleEntity.StartTimeTheSubject)
                              .TotalSeconds) * 100).Sum();
                var percentageOfStreamActivity = item.Select(
                    x => (GetStreamOperationTime(item).TotalSeconds /
                          x.ScheduleEntity.EndTimeTheSubject.Subtract(x.ScheduleEntity.StartTimeTheSubject)
                              .TotalSeconds) * 100).Sum();

                var percentageOfVideoActivity = item.Select(
                    x => (GetCameraOperationTime(item).TotalSeconds /
                          x.ScheduleEntity.EndTimeTheSubject.Subtract(x.ScheduleEntity.StartTimeTheSubject)
                              .TotalSeconds) * 100).Sum();

                var percentageOfSelfDeafenedActivity = item.Select(
                    x => (GetSelfDeafenedOperationTime(item).TotalSeconds /
                          x.ScheduleEntity.EndTimeTheSubject.Subtract(x.ScheduleEntity.StartTimeTheSubject)
                              .TotalSeconds) * 100).Sum();

                users.Add(new StatisticDto.StatisticByUser
                {
                    SubjectName = item.Key,
                    UserName = item.First().UserEntity.Nickname!,
                    GroupName = item.First().UserEntity.GroupEntity!.GroupName,
                    PercentageOfAttendance = percentageOfAttendance,
                    PercentageOfMicrophoneActivity = percentageOfMicrophoneActivity,
                    PercentageOfVideoActivity = percentageOfVideoActivity,
                    PercentageOfSelfDeafenedActivity = percentageOfSelfDeafenedActivity,
                    PercentageOfStreamActivity = percentageOfStreamActivity
                });
            }

            return Ok(users);
        }
        private static TimeSpan GetMicrophoneOperationTime(IEnumerable<StatisticModel> item)
        {
            var microphoneOperatingTime = new TimeSpan();
            microphoneOperatingTime = item
                .SelectMany(statistic => statistic.MicrophoneActionsEntity)
                .Aggregate(microphoneOperatingTime, (current, microphoneActionsEntity) => current + (microphoneActionsEntity.MicrophoneOperatingTime ?? new TimeSpan()));

            return microphoneOperatingTime;
        }

        private static TimeSpan GetCameraOperationTime(IEnumerable<StatisticModel> item)
        {
            var cameraOperatingTime = new TimeSpan();
            cameraOperatingTime = item
                .SelectMany(statistic => statistic.CameraActionsEntity)
                .Aggregate(cameraOperatingTime, (current, cameraActionsEntity) => current + (cameraActionsEntity.CameraOperationTime ?? new TimeSpan()));

            return cameraOperatingTime;
        }
        private static TimeSpan GetStreamOperationTime(IEnumerable<StatisticModel> item)
        {
            var streamOperatingTime = new TimeSpan();
            streamOperatingTime = item
                .SelectMany(statistic => statistic.StreamActionsEntity)
                .Aggregate(streamOperatingTime, (current, streamActionsEntity) => current + (streamActionsEntity.StreamOperationTime ?? new TimeSpan()));

            return streamOperatingTime;
        }
        private static TimeSpan GetSelfDeafenedOperationTime(IEnumerable<StatisticModel> item)
        {
            var selfDeafenedOperatingTime = new TimeSpan();
            selfDeafenedOperatingTime = item
                .SelectMany(statistic => statistic.SelfDeafenedActionsEntities)
                .Aggregate(selfDeafenedOperatingTime, (current, selfDeafenedActionsEntity) => current + (selfDeafenedActionsEntity.SelfDeafenedOperationTime ?? new TimeSpan()));

            return selfDeafenedOperatingTime;
        }
    }
}