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
        public async Task AddScheduleForOneSubject(ScheduleEntity scheduleEntity)
        {
            var entity = new ScheduleEntity
            {
                StartTimeTheSubject = scheduleEntity.StartTimeTheSubject,
                EndTimeTheSubject = scheduleEntity.EndTimeTheSubject,
                ChannelId = scheduleEntity.ChannelId,
                DayOfTheWeek = scheduleEntity.DayOfTheWeek
            };
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
