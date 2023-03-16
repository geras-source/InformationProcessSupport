using ClosedXML.Excel;
using InformationProcessSupport.Core.Statistics;
using InformationProcessSupport.Core.StatisticsCollector.Extensions;
using InformationProcessSupport.Core.Users;
using System.Diagnostics;
using System.Text;

namespace InformationProcessSupport.Core.StatisticsCollector
{
    public class StatisticCollectorServices : IStatisticCollectorServices
    {
        private readonly IStatisticRepository _statisticRepository;
        private readonly IUserRepository _userRepository;

        public StatisticCollectorServices(IStatisticRepository statisticRepository, IUserRepository userRepository)
        {
            _statisticRepository = statisticRepository;
            _userRepository = userRepository;
        }

        public async Task CreateReportByDate(string date)
        {
            var statisticEntities = await _statisticRepository.GetStatisticCollectionsByDateAsync(date);
            var userEntities = await _userRepository.GetUserCollectionAsync();
            var statistic = statisticEntities.ConvertToDto(userEntities);

            var entities1 = _statisticRepository.GetStatisticCollectionsAsync().Result;
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sample Sheet");
                int i = 1;
                foreach(var entity in entities1)
                {
                    worksheet.Cell(i, 1).Value = entity.ConnectionTime;
                    worksheet.Cell(i, 2).Value = entity.EntryTime;
                    worksheet.Cell(i, 3).Value = entity.ExitTime;
                    worksheet.Cell(i, 4).Value = entity.UserId;
                    worksheet.Cell(i, 5).Value = entity.ChannelId;
                    i++;
                }
                
                workbook.SaveAs(@"C:\HelloWorld.xlsx");
                Process.Start(new ProcessStartInfo(@"C:\HelloWorld.xlsx") { UseShellExecute = true });
            }
        }

        public async Task CreateReportByTemplate()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sample Sheet");
                    worksheet.Cell(1, 5).Value = "Hello World!";
                    worksheet.Cell("A2").FormulaA1 = "=MID(A1, 7, 5)";
                    workbook.SaveAs(@"C:\HelloWorld.xlsx");
                    Process.Start(new ProcessStartInfo(@"C:\HelloWorld.xlsx") { UseShellExecute = true });
                }
            }
            catch (Exception e) { Console.WriteLine(e); }

        }
    }
}