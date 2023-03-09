using InformationProcessSupport.Core.ScheduleParser;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;

namespace InformationProcessSupport.Server.Controllers
{
    [ApiController]
    [Route("[api/controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IParserServices _parserServices;
        IWebHostEnvironment _appEnvironment;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IParserServices parserServices, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _parserServices= parserServices;
            _appEnvironment = appEnvironment;
        }

        //[HttpPost("GetWeatherForecast")]
        //public async Task Post(ScheduleEntity scheduleEntity)
        //{
        //    var entity = new ScheduleEntity
        //    {
        //        SubjectName = scheduleEntity.SubjectName,
        //        StartTimeTheSubject = scheduleEntity.StartTimeTheSubject
        //    };
        //    Console.WriteLine(entity.SubjectName.ToString());
        //    Console.WriteLine(entity.StartTimeTheSubject.ToString());
        //}
    }
}