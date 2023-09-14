using InformationProcessSupport.Core.ScheduleParser;
using InformationProcessSupport.Server.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using InformationProcessSupport.Core.Domains;

namespace InformationProcessSupport.Server.Controllers.Schedules
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IParserServices _parserServices;
        private readonly IStorageProvider _storageProvider;

        public ScheduleController(IParserServices parserServices, IStorageProvider storageProvider)
        {
            _parserServices = parserServices;
            _storageProvider = storageProvider;
        }

        [HttpPost("PostScheduleCollection")]
        public async Task<ActionResult> PostScheduleCollectionToParsing([FromBody] IEnumerable<ScheduleDto> scheduleCollection) // Done
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
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (WebException ex)
                {
                    return StatusCode(500, ex.Status);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }

                return Ok();
            }
        }
        [HttpPost("PostGroupCollection")]
        public async Task<ActionResult> PostGroupToDB([FromBody] IEnumerable<GroupDto> groupCollection) //Done
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
                await _storageProvider.AddGroupCollectionAsync(entities);
                return Ok();
            }
        }
    }
}