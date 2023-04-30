using ClosedXML.Excel;
using InformationProcessSupport.Core.Domains;
using InformationProcessSupport.Core.StatisticsCollector.Extensions;

namespace InformationProcessSupport.Core.StatisticsCollector
{
    public class StatisticCollectorServices : IStatisticCollectorServices
    {
        private readonly IStorageProvider _storageProvider;
        private const int Indentantion = 4;

        public StatisticCollectorServices(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        public async Task<XLWorkbook> CreateReportByDate(DateTime date)
        {
            var statisticEntities = await _storageProvider.GetStatisticCollectionsByDateAsync(date);
            var userEntities = await _storageProvider.GetUserCollectionAsync();
            var channelEntities = await _storageProvider.GetChannelCollectionAsync();
            var groupEntities = await _storageProvider.GetGroupCollectionAsync();
            var microphoneEntities = await _storageProvider.GetMicrophoneCollectionAsync();
            var cameraEntities = await _storageProvider.GetCameraCollectionAsync();
            var streamEntities = await _storageProvider.GetStreamCollectionAsync();
            var selfDeafenedEntities = await _storageProvider.GetSelfDeafenedCollectionAsync();
            var scheduleEntities = await _storageProvider.GetScheduleCollectionAsync();

            var userStatistics = statisticEntities.
                CollectStatistics(userEntities, channelEntities, groupEntities, microphoneEntities, cameraEntities, selfDeafenedEntities, streamEntities, scheduleEntities);

            var workbook = new XLWorkbook();
            
            var groupByGroupNames = userStatistics.GroupBy(x => x.GroupName);

            foreach (var group in groupByGroupNames)
            {
                GenerateStatisticsWorksheets(group, workbook);
            }
            
            return workbook;
        }
        private static void GenerateStatisticsWorksheets(IGrouping<string, GeneratedStatistics> group, IXLWorkbook workbook)
        {
            var row = 2;

            var worksheet = workbook.Worksheets.Add(group.Key);
            var groupByChannelNames = group.GroupBy(x => x.SubjectName);

            foreach (var channel in groupByChannelNames)
            {
                SetHeaderStatistic(worksheet, row);
                SetHeaderSchedule(worksheet, row);

                SetScheduleData(worksheet, row, channel);

                var users = ConvertStatistics(channel);

                foreach (var user in users)
                {
                    SetStatisticData(worksheet, row, user);
                    row++;
                }
                row += Indentantion;
            }
            worksheet.Columns().AdjustToContents();
        }
        private static void SetHeaderSchedule(IXLWorksheet worksheet, int row)
        {
            var headers = new List<string>()
            {
                "Название канала",
                "Название предмета",
                "Время пары"
            };
            for (var i = 1; i < headers.Count + 1; i++)
            {
                worksheet.Cell(row - 1, i).Value = headers[i - 1];
            }

            worksheet.Range($"C{row - 1}:D{row - 1}").Row(1).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        }
        private static void SetHeaderStatistic(IXLWorksheet worksheet, int row)
        {
            var headers = new List<string>()
            {
                "Имя студента",
                "Сколько был подключен",
                "Время входа",
                "Время выхода",
                "Кол-во входов/выходов",
                "Опоздание",
                "Время выключенного микрофона",
                "Время включенной камеры",
                "Время включенного стрима",
                "Время выключенного звука"
            };
            for (var i = 1; i < headers.Count + 1; i++)
            {
                worksheet.Cell(row + 1, i).Value = headers[i - 1];
            }
        }
        private static void SetScheduleData(IXLWorksheet worksheet, int row, IGrouping<string, GeneratedStatistics> channel)
        {
            var subject = channel.FirstOrDefault(x => x.SubjectName == channel.Key); // TODO: ???

            worksheet.Cell(row, 1).Value = subject?.ChannelName;
            worksheet.Cell(row, 2).Value = channel.Key;
            worksheet.Cell(row, 3).Value = subject?.StartTimeTheSubject;
            worksheet.Cell(row, 4).Value = subject?.EndTimeTheSubject;
        }
        private static void SetStatisticData(IXLWorksheet worksheet, int row, GeneratedStatistics statistics)
        {
            worksheet.Cell(row + 2, 1).Value = statistics.UserName;
            worksheet.Cell(row + 2, 2).Value = statistics.ConnectionTime;
            worksheet.Cell(row + 2, 2).Style.Fill.BackgroundColor = SetColorForConnectionTime(statistics);
            worksheet.Cell(row + 2, 3).Value = statistics.EntryTime;
            worksheet.Cell(row + 2, 4).Value = statistics.ExitTime;
            worksheet.Cell(row + 2, 5).Value = statistics.ExitCount;
            worksheet.Cell(row + 2, 6).Value = statistics.Attendance;
            worksheet.Cell(row + 2, 6).Style.Fill.BackgroundColor = SetColorForAttendance(statistics.Attendance);
            worksheet.Cell(row + 2, 7).Value = statistics.MicrophoneOperatingTime;
            worksheet.Cell(row + 2, 8).Value = statistics.CameraOperatingTime;
            worksheet.Cell(row + 2, 9).Value = statistics.StreamOperatingTime;
            worksheet.Cell(row + 2, 10).Value = statistics.SelfDeafenedOperatingTime;
        }
        private static XLColor SetColorForAttendance(string attendance)
        {
            if (attendance == Attedance.Вовермя.ToString())
                return XLColor.GreenPigment;

            if (attendance == Attedance.Опоздал.ToString())
                return XLColor.Red;

            return XLColor.NoColor;
        }
        private static XLColor SetColorForConnectionTime(GeneratedStatistics statistics)
        {
            var subjectDurationMinutes = statistics.EndTimeTheSubject.Subtract(statistics.StartTimeTheSubject).TotalMinutes;
            var connectionDurationMinutes = statistics.ConnectionTime.TotalMinutes;
            var connectionPercent = connectionDurationMinutes / subjectDurationMinutes * 100;
            return connectionPercent switch
            {
                > 0 and <= 25 => XLColor.Red,
                > 25 and <= 50 => XLColor.InternationalOrange,
                > 50 and <= 75 => XLColor.CadmiumYellow,
                > 75 and <= 100 => XLColor.GreenPigment,
                _ => XLColor.NoColor
            };
        }
        private static IEnumerable<GeneratedStatistics> ConvertStatistics(IEnumerable<GeneratedStatistics> statistics)
        {
            var generatedStatistics = new List<GeneratedStatistics>();
            var groupByUserNames = statistics.GroupBy(x => x.UserName);

            foreach (var item in groupByUserNames)
            {
                var connectionTime = GetConnectionTime(item);
                var microphoneOperatingTime = GetMicrophoneOperationTime(item);
                var cameraOperationTime = GetCameraOperationTime(item);
                var streamOperationTime = GetStreamOperationTime(item);
                var selfDeafenedOperationTime = GetSelfDeafenedOperationTime(item);
               
                generatedStatistics.Add(new GeneratedStatistics
                {
                    Attendance = item.First().Attendance,
                    ConnectionTime = connectionTime,
                    EntryTime = item.First().EntryTime,
                    ExitTime = item.Last().ExitTime,
                    UserName = item.First().UserName,
                    EntryCount = item.Count(),
                    ExitCount = item.Count(),
                    GroupName = item.First().GroupName,
                    ChannelName = item.First().ChannelName,
                    StartTimeTheSubject = item.First().StartTimeTheSubject,
                    EndTimeTheSubject = item.First().EndTimeTheSubject,
                    MicrophoneOperatingTime = microphoneOperatingTime,
                    CameraOperatingTime = cameraOperationTime,
                    StreamOperatingTime = streamOperationTime,
                    SelfDeafenedOperatingTime = selfDeafenedOperationTime
                });
            }
            return generatedStatistics;
        }

        private static TimeSpan GetConnectionTime(IEnumerable<GeneratedStatistics> item)
        {
            var connectionTime = new TimeSpan();
            connectionTime = item.Aggregate(connectionTime, (current, user) => current + user.ConnectionTime);
            return connectionTime;
        }

        private static TimeSpan GetMicrophoneOperationTime(IEnumerable<GeneratedStatistics> item)
        {
            var microphoneOperatingTime = new TimeSpan();
            microphoneOperatingTime = item
                .SelectMany(statistic => statistic.MicrophoneActionsEntity)
                .Aggregate(microphoneOperatingTime, (current, microphoneActionsEntity) => current + (microphoneActionsEntity.MicrophoneOperatingTime ?? new TimeSpan()));

            return microphoneOperatingTime;
        }

        private static TimeSpan GetCameraOperationTime(IEnumerable<GeneratedStatistics> item)
        {
            var cameraOperatingTime = new TimeSpan();
            cameraOperatingTime = item
                .SelectMany(statistic => statistic.CameraActionsEntity)
                .Aggregate(cameraOperatingTime, (current, cameraActionsEntity) => current + (cameraActionsEntity.CameraOperationTime ?? new TimeSpan()));

            return cameraOperatingTime;
        }
        private static TimeSpan GetStreamOperationTime(IEnumerable<GeneratedStatistics> item)
        {
            var streamOperatingTime = new TimeSpan();
            streamOperatingTime = item
                .SelectMany(statistic => statistic.StreamActionsEntity)
                .Aggregate(streamOperatingTime, (current, streamActionsEntity) => current + (streamActionsEntity.StreamOperationTime ?? new TimeSpan()));

            return streamOperatingTime;
        }
        private static TimeSpan GetSelfDeafenedOperationTime(IEnumerable<GeneratedStatistics> item)
        {
            var selfDeafenedOperatingTime = new TimeSpan();
            selfDeafenedOperatingTime = item
                .SelectMany(statistic => statistic.SelfDeafenedActionsEntities)
                .Aggregate(selfDeafenedOperatingTime, (current, selfDeafenedActionsEntity) => current + (selfDeafenedActionsEntity.SelfDeafenedOperationTime ?? new TimeSpan()));

            return selfDeafenedOperatingTime;
        }
        private enum Attedance
        {
            Опоздал = 0,
            Вовермя = 2
        }
    }
}