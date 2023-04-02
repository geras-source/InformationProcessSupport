using InformationProcessSupport.Core.Groups;
using InformationProcessSupport.Core.ScheduleParser;
using InformationProcessSupport.Server.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InformationProcessSupport.Server.Controllers.Schedules
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IParserServices _parserServices;
        private readonly IGroupRepository _groupRepository;

        public ScheduleController(IParserServices parserServices, IGroupRepository groupRepository)
        {
            _parserServices = parserServices;
            _groupRepository = groupRepository;
        }

        [HttpPost("PostScheduleCollection")]
        public async Task<ActionResult> PostScheduleCollectionToParsing([FromBody] ICollection<ScheduleDto> scheduleCollection) // Done
        {
            if (scheduleCollection == null)
            {
                return NoContent();
            }
            else
            {
                var entities = scheduleCollection.Select(x => new Schedule
                {
                    SubjectName = x.SubjectName,
                    StartTimeTheSubject = x.StartTimeTheSubject,
                    EndTimeTheSubject = x.EndTimeTheSubject,
                    DayOfTheWeek = x.DayOfTheWeek,
                    GroupName = x.GroupName, // TODO: сделать массивом
                    Lecturer = x.Lecturer
                }).ToList();
                try
                {
                    await _parserServices.ParseScheduleCollection(entities);
                }
                catch(ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (WebException ex)
                {
                    return StatusCode(500, ex.Status);
                }

                return Ok();
            }
        }
        [HttpPost("PostGroupCollection")]
        public async Task<ActionResult> PostGroupToDB([FromBody] ICollection<GroupDto> groupCollection) //Done
        {
            if (groupCollection == null)
            {
                return NotFound();
            }
            else
            {
                var entities = groupCollection.Select(x => new GroupEntity
                {
                    GroupName = x.GroupName,
                    GuildName = x.GuildName
                }).ToList();
                await _groupRepository.AddCollectionGroupAsync(entities);
                return Ok();
            }
        }
    }
}