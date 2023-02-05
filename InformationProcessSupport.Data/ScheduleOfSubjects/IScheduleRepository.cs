using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProcessSupport.Data.ScheduleOfSubjects
{
    public interface IScheduleRepository
    {
        Task AddScheduleForOneSubject(ScheduleEntity scheduleEntity);
    }
}
