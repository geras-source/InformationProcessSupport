using InformationProcessSupport.Core.StatisticsCollector;
using Microsoft.AspNetCore.Mvc;

namespace InformationProcessSupport.Server.Controllers.Statistics
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticCollectorServices _collectorServices;
        public StatisticController(IStatisticCollectorServices collectorServices)
        {
            _collectorServices= collectorServices;
        }
        [HttpGet("DownloadTheExcelFile")]
        public async Task<IActionResult> DownloadfromBytes([FromQuery]DateTime date)
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "testBook.xlsx";
            var memoryStream = new MemoryStream();
            var result = await _collectorServices.CreateReportByDate(date);
            result.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new FileStreamResult(memoryStream, contentType)
            {
                FileDownloadName = fileName
            };
        }
    }
}
