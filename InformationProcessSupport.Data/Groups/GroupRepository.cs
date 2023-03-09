using InformationProcessSupport.Core.Groups;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProcessSupport.Data.Groups
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationContext _context;
        public GroupRepository(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task AddAsync(GroupEntity groupEntity)
        {
            var entity = new GroupModel
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
            var entities = groupEntities.Select(x => new GroupModel
            {
                GroupName = x.GroupName,
                AlternateKey = x.AlternateKey,
                GuildId = x.GuildId,
                GuildName = x.GuildName                
            }).ToList();
            await _context.GroupEntities.AddRangeAsync(entities);
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

            return entity.GroupId;
        }

        public async Task<int> GetGroupIdByName(string groupName)
        {
            var groupId = await _context.GroupEntities.SingleAsync(x => x.GroupName == groupName);

            return groupId.GroupId;
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
