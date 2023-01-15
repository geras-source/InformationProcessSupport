﻿namespace InformationProcessSupport.Data.Statistics
{
    public interface IStatisticRepository
    {
        Task AddAsync(StatisticEntity statisticEntity);
        Task UpdateConnectionTimeAsync(StatisticEntity statisticEntity);
        Task<int> GetStatisticIdByUserIdAndChannelId(ulong userId, ulong channelId);
    }
}