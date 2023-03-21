using InformationProcessSupport.Core.Groups;
using InformationProcessSupport.Core.ScheduleParser;
using InformationProcessSupport.Core.StatisticsCollector;
using InformationProcessSupport.Server.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InformationProcessSupport.Server.Controllers.Schedules
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IParserServices _parserServices;
        private readonly IGroupRepository _groupRepository;
        private readonly IStatisticCollectorServices _statisticCollectorServices;

        public ScheduleController(IParserServices parserServices, IGroupRepository groupRepository, IStatisticCollectorServices statisticCollectorServices)
        {
            _parserServices = parserServices;
            _groupRepository = groupRepository;
            _statisticCollectorServices = statisticCollectorServices;
        }

        [HttpPost("PostScheduleCollection")]
        public async Task<ActionResult> PostScheduleCollectionToParsing(ICollection<ScheduleDto> scheduleCollection)
        {
            if (scheduleCollection == null)
            {
                return NotFound();
            }
            else
            {
                var entities = scheduleCollection.Select(x => new Schedule
                {
                    SubjectName = x.SubjectName,
                    StartTimeTheSubject = x.StartTimeTheSubject,
                    EndTimeTheSubject = x.EndTimeTheSubject,
                    DayOfTheWeek = x.DayOfTheWeek,
                    GroupName = x.GroupName,
                    Lecturer = x.Lecturer
                }).ToList();
               
                await _parserServices.ParseScheduleCollection(entities);
                return Ok();
            }
        }
        [HttpPost("PostGroupCollection")]
        public async Task<ActionResult> PostGroupToDB(ICollection<GroupDto> groupCollection)
        {
            await _statisticCollectorServices.CreateReportByDate("2023.04.02");
            if (groupCollection == null)
            {
                return NotFound();
            }
            else
            {
                //groupId
                //key
                var entities = groupCollection.Select(x => new GroupEntity
                {
                    GroupName = x.GroupName,
                    GuildName = x.GuildName
                }).ToList();
                //_groupRepository.AddRangeGroups(entities);
                return Ok();
            }
        }
    }
}
