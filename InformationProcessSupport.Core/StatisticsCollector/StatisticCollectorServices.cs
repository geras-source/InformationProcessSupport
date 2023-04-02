using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Wordprocessing;
using InformationProcessSupport.Core.Channels;
using InformationProcessSupport.Core.Groups;
using InformationProcessSupport.Core.Statistics;
using InformationProcessSupport.Core.StatisticsCollector.Extensions;
using InformationProcessSupport.Core.TimeOfActionsInTheChannel.CameraActions;
using InformationProcessSupport.Core.TimeOfActionsInTheChannel.MicrophoneActions;
using InformationProcessSupport.Core.TimeOfActionsInTheChannel.SelfDeafenedActions;
using InformationProcessSupport.Core.TimeOfActionsInTheChannel.StreamActions;
using InformationProcessSupport.Core.Users;
using System.Diagnostics;
using System.Text;

namespace InformationProcessSupport.Core.StatisticsCollector
{
    public class StatisticCollectorServices : IStatisticCollectorServices
    {
        private const int INDENTANTION = 4;
        private readonly IStatisticRepository _statisticRepository;
        private readonly IUserRepository _userRepository;
        private readonly IChannelRepository _channelRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ICameraActionRepository _cameraActionRepository;
        private readonly IMicrophoneActionsRepository _microphoneActionsRepository;
        private readonly ISelfDeafenedActionsRepository _selfDeafenedActionsRepository;
        private readonly IStreamActionsRepository _streamActionsRepository;

        public StatisticCollectorServices(IStatisticRepository statisticRepository, IUserRepository userRepository, IChannelRepository channelRepository,
            IGroupRepository groupRepository, ICameraActionRepository cameraActionRepository, IMicrophoneActionsRepository microphoneActionsRepository,
            ISelfDeafenedActionsRepository selfDeafenedActionsRepository, IStreamActionsRepository streamActionsRepository)
        {
            _statisticRepository = statisticRepository;
            _userRepository = userRepository;
            _channelRepository = channelRepository;
            _groupRepository = groupRepository;
            _cameraActionRepository = cameraActionRepository;
            _microphoneActionsRepository = microphoneActionsRepository;
            _selfDeafenedActionsRepository = selfDeafenedActionsRepository;
            _streamActionsRepository = streamActionsRepository;
        }

        public async Task<XLWorkbook> CreateReportByDate(string date)
        {
            var statisticEntities = await _statisticRepository.GetStatisticCollectionsAsync();
            var userEntities = await _userRepository.GetUserCollectionAsync();
            var chanellEntities = await _channelRepository.GetCollectionChannelAsync();
            var groupEntities = await _groupRepository.GetGroupCollectionAsync();

            var statistic = statisticEntities.CollectStatistics(userEntities, chanellEntities, groupEntities);

            var workbook = new XLWorkbook();
            
            var groupByGroups = statistic.GroupBy(x => x.GroupName); // группировать по группам

            foreach (var group in groupByGroups)
            {
                var worksheet = workbook.Worksheets.Add(group.Key);

                var groupByChannels = group.GroupBy(x => x.ChannelName);
                int row = 2;
                foreach (var channelitem in groupByChannels)
                {
                    worksheet.Cell(row, 1).Value = channelitem.Key;                     // разбить на методы
                    worksheet.Cell(row - 1, 1).Value = "Название предмета";
                    worksheet.Cell(row + 1, 1).Value = "Имя студента";
                    worksheet.Cell(row + 1, 2).Value = "Сколько был подключен";
                    worksheet.Cell(row + 1, 3).Value = "Время входа ";
                    worksheet.Cell(row + 1, 4).Value = "Время выхода ";
                    worksheet.Cell(row + 1, 5).Value = "Кол-во входов/выходов ";

                    var user = Convertor(channelitem);

                    foreach (var entity in user)
                    {
                        worksheet.Cell(row + 2, 1).Value = entity.UserName;
                        //worksheet.Cell(row + 2, 1).Style.Fill.BackgroundColor = XLColor.Red;
                        worksheet.Cell(row + 2, 2).Value = entity.ConnectionTime;
                        worksheet.Cell(row + 2, 3).Value = entity.EntryTime;
                        worksheet.Cell(row + 2, 4).Value = entity.ExitTime;
                        worksheet.Cell(row + 2, 5).Value = entity.ExitCount;
                        row++;
                    }
                    row += INDENTANTION;
                }
                worksheet.Columns().AdjustToContents();
            }
            
            return workbook;
        }

        private IEnumerable<GeneratedStatistics> Convertor(IEnumerable<GeneratedStatistics> statistics)
        {
            List<GeneratedStatistics> generatedStatistics = new();
            var groupByUserNames = statistics.GroupBy(x => x.UserName);
            
            foreach(var item in groupByUserNames)
            {
                TimeSpan connectionTime = new TimeSpan();
                int countEntry = 0;
                int countExit = 0;
                foreach(var user in item)
                {
                    connectionTime += user.ConnectionTime;
                    countEntry++;
                    countExit++;
                }
                generatedStatistics.Add(new GeneratedStatistics
                {
                    ConnectionTime = connectionTime,
                    EntryTime = item.First().EntryTime,
                    ExitTime = item.Last().ExitTime,
                    UserName = item.First().UserName,
                    EntryCount = countEntry,
                    ExitCount = countExit,
                    GroupName = item.First().GroupName,
                    ChannelName = item.First().ChannelName
                });
            }
            return generatedStatistics;
        }

        public async Task CreateReportByTemplate()
        {
            
        }
    }
}