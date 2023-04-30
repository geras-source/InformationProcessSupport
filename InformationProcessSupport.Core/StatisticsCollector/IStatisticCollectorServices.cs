using ClosedXML.Excel;

namespace InformationProcessSupport.Core.StatisticsCollector
{
    public interface IStatisticCollectorServices
    {
        Task<XLWorkbook> CreateReportByDate(DateTime date);
    }
}