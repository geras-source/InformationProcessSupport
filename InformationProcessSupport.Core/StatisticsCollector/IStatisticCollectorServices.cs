using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProcessSupport.Core.StatisticsCollector
{
    public interface IStatisticCollectorServices
    {
        Task CreateReportByTemplate();
        Task<XLWorkbook> CreateReportByDate(string date);
    }
}
