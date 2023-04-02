using InformationProcessSupport.Core.Channels;
using InformationProcessSupport.Core.Groups;
using InformationProcessSupport.Core.Statistics;
using InformationProcessSupport.Core.Users;

namespace InformationProcessSupport.Core.StatisticsCollector.Extensions
{
    internal static class StatisticConversions
    {
        public static IEnumerable<GeneratedStatistics> CollectStatistics(this IEnumerable<StatisticEntity> statisticCollection, IEnumerable<UserEntity> userCollection,
            IEnumerable<ChannelEntity> channelEntities, IEnumerable<GroupEntity> groupEntities)
        {
            return (from statisticItem in statisticCollection
                    join userItem in userCollection on statisticItem.UserId equals userItem.UserId
                    join channleItem in channelEntities on statisticItem.ChannelId equals channleItem.ChannelId
                    join groupItem in groupEntities on userItem.GroupId equals groupItem.GroupId
                    select new GeneratedStatistics
                    {
                        UserName = userItem.Nickname,
                        ConnectionTime = statisticItem.ConnectionTime,
                        Attendance = statisticItem.Attendance,
                        ChannelName = channleItem.Name,
                        GroupName = groupItem.GroupName,
                        EntryTime = statisticItem.EntryTime,
                        ExitTime = statisticItem.ExitTime
                    }).ToList();
        }
    }
}