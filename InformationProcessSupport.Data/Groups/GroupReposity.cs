using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProcessSupport.Data.Groups
{
    public class GroupReposity : IGroupReposity
    {
        private readonly ApplicationContext _context;
        public GroupReposity(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task AddAsync(GroupEntity groupEntity)
        {
            var entity = new GroupEntity
            {
                GroupName = groupEntity.GroupName,
                AlternateKey = groupEntity.AlternateKey,
                GuildId = groupEntity.GuildId,
                GuildName = groupEntity.GuildName
            };
            await _context.GroupEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeGroups(List<GroupEntity> groupEntities)
        {
            await _context.GroupEntities.AddRangeAsync(groupEntities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int groupId)
        {
            var entity = await _context.GroupEntities.SingleOrDefaultAsync(x => x.GroupId == groupId);
            
            if(entity != null)
            {
                _context.GroupEntities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetGroupIdByAlternateId(ulong alternateId, ulong guildId)
        {
            var entity = await _context.GroupEntities.SingleAsync(x => x.AlternateKey == alternateId && x.GuildId == guildId);
            var resultId = entity.GroupId;
            return resultId;
        }

        public async Task UpdateAsync(GroupEntity groupEntity)
        {
            var entity = await _context.GroupEntities.SingleAsync(x => x.GroupId == groupEntity.GroupId);

            if(entity != null )
            { 
                entity.GroupName = groupEntity.GroupName;
                await _context.SaveChangesAsync();
            }
        }
    }
}
