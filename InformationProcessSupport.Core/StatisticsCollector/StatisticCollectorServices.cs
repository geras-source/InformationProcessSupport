using ClosedXML.Excel;
using InformationProcessSupport.Core.Statistics;
using System.Diagnostics;
using System.Text;

namespace InformationProcessSupport.Core.StatisticsCollector
{
    public class StatisticCollectorServices : IStatisticCollectorServices
    {
        private readonly IStatisticRepository _statisticRepository;

        public StatisticCollectorServices(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }
        public Task CreaCreateReport()
        {
            throw new NotImplementedException();
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
