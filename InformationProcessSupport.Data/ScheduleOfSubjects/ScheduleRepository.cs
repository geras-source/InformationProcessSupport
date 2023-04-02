using InformationProcessSupport.Core.ScheduleOfSubjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProcessSupport.Data.ScheduleOfSubjects
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationContext _context;
        public ScheduleRepository(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task AddScheduleCollectionAsync(ICollection<ScheduleEntity> scheduleCollection)
        {
            var entities = scheduleCollection.Select(x => new ScheduleModel
            {
                SubjectName = x.SubjectName,
                StartTimeTheSubject = x.StartTimeTheSubject,
                EndTimeTheSubject = x.EndTimeTheSubject,
                DayOfTheWeek = x.DayOfTheWeek,
                GroupId = x.GroupId,
                ChannelId = x.ChannelId
            }).ToList();
            await _context.ScheduleEntities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task AddScheduleForOneSubject(ScheduleEntity scheduleEntity)
        {
            var entity = new ScheduleModel
            {
                SubjectName = scheduleEntity.SubjectName,
                StartTimeTheSubject = scheduleEntity.StartTimeTheSubject,
                EndTimeTheSubject = scheduleEntity.EndTimeTheSubject,
                ChannelId = scheduleEntity.ChannelId,
                DayOfTheWeek = scheduleEntity.DayOfTheWeek,
                GroupId= scheduleEntity.GroupId
            };
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ScheduleEntity> GetScheduleByChannelIdAsync(int channelId)
        {
            var model = await _context.ScheduleEntities.FirstOrDefaultAsync(x => x.ChannelId == channelId);

            var entity = new ScheduleEntity
            {
                DayOfTheWeek = model.DayOfTheWeek,
                StartTimeTheSubject = model.StartTimeTheSubject,
                EndTimeTheSubject = model.EndTimeTheSubject,
                SubjectName = model.SubjectName
            };

            return entity;
        }
    }
}
