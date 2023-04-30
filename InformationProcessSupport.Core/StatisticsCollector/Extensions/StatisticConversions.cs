using InformationProcessSupport.Core.Domains;

namespace InformationProcessSupport.Core.StatisticsCollector.Extensions
{
    internal static class StatisticConversions
    {
        public static IEnumerable<GeneratedStatistics> CollectStatistics(this IEnumerable<StatisticEntity> statisticCollection,
            IEnumerable<UserEntity> userCollection,
            IEnumerable<ChannelEntity> channelEntities, 
            IEnumerable<GroupEntity> groupEntities, 
            IEnumerable<MicrophoneActionsEntity> microphoneActions,
            IEnumerable<CameraActionsEntity> cameraActions, 
            IEnumerable<SelfDeafenedActionsEntity> selfDeafenedActions, 
            IEnumerable<StreamActionsEntity> streamActions,
            IEnumerable<ScheduleEntity> scheduleEntities)
        {
            var generatedStatistics = (from statisticItem in statisticCollection
                join userItem in userCollection on statisticItem.UserId equals userItem.UserId
                join channelItem in channelEntities on statisticItem.ChannelId equals channelItem.ChannelId
                join groupItem in groupEntities on userItem.GroupId equals groupItem.GroupId
                join scheduleItem in scheduleEntities on statisticItem.SheduleId equals scheduleItem.ScheduleId
                select new GeneratedStatistics
                {
                    Id = statisticItem.StatisticId,
                    UserName = userItem.Nickname,
                    ConnectionTime = statisticItem.ConnectionTime,
                    Attendance = statisticItem.Attendance,
                    ChannelName = channelItem.Name,
                    GroupName = groupItem.GroupName,
                    EntryTime = statisticItem.EntryTime,
                    ExitTime = statisticItem.ExitTime,
                    SubjectName = scheduleItem.SubjectName,
                    StartTimeTheSubject = scheduleItem.StartTimeTheSubject,
                    EndTimeTheSubject = scheduleItem.EndTimeTheSubject,
                    MicrophoneActionsEntity = new List<MicrophoneActionsEntity>(),
                    CameraActionsEntity = new List<CameraActionsEntity>(),
                    StreamActionsEntity = new List<StreamActionsEntity>(),
                    SelfDeafenedActionsEntities = new List<SelfDeafenedActionsEntity>()
                }).ToList();

            foreach (var group in microphoneActions.GroupBy(ma => ma.StatisticId))
            {
                var statistic = generatedStatistics.FirstOrDefault(s => s.Id == group.Key);
                statistic?.MicrophoneActionsEntity.AddRange(group);
            }

            foreach (var group in cameraActions.GroupBy(ca => ca.StatisticId))
            {
                var statistic = generatedStatistics.FirstOrDefault(s => s.Id == group.Key);
                statistic?.CameraActionsEntity.AddRange(group);
            }

            foreach (var group in streamActions.GroupBy(sa => sa.StatisticId))
            {
                var statistic = generatedStatistics.FirstOrDefault(s => s.Id == group.Key);
                statistic?.StreamActionsEntity.AddRange(group);
            }

            foreach (var group in selfDeafenedActions.GroupBy(sa => sa.StatisticId))
            {
                var statistic = generatedStatistics.FirstOrDefault(s => s.Id == group.Key);
                statistic?.SelfDeafenedActionsEntities.AddRange(group);
            }

            return generatedStatistics;
        }
    }
}