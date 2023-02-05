using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProcessSupport.Data.TimeOfActionsInTheChannel
{
    public class TimeActionsGenericRepository<TEntity> : ITimeActionsGenericRepository<TEntity> where TEntity: class
    {
        private readonly ApplicationContext _context;
        public TimeActionsGenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddendumAOperatingTime(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddTurnOffTimeAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}