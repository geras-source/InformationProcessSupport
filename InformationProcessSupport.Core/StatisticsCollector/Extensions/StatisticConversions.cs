using InformationProcessSupport.Core.Statistics;
using InformationProcessSupport.Core.Users;

namespace InformationProcessSupport.Core.StatisticsCollector.Extensions
{
    internal static class StatisticConversions
    {
        public static IEnumerable<GeneratedStatistics> ConvertToDto(this IEnumerable<StatisticEntity> statisticCollection, IEnumerable<UserEntity> userCollection)
        {
            return (from statisticItem in statisticCollection
                    join userItem in userCollection
                    on statisticItem.UserId equals userItem.UserId
                    select new GeneratedStatistics
                    {
                        UserName = userItem.Name,
                        ConnectionTime = statisticItem.ConnectionTime,
                        Attendance = statisticItem.Attendance
                    }).ToList();
        }
    }
}
