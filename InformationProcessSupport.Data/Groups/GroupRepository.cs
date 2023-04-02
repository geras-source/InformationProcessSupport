using InformationProcessSupport.Core.Groups;
using Microsoft.EntityFrameworkCore;

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

        public async Task AddCollectionGroupAsync(List<GroupEntity> groupEntities)
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

        public async Task<ICollection<GroupEntity>> GetGroupCollectionAsync()
        {
            var entities = await _context.GroupEntities.Select(it => new GroupEntity
            {
                GroupId = it.GroupId,
                GroupName = it.GroupName
            }).ToListAsync();

            return entities;
        }

        public async Task<int> GetGroupIdByAlternateId(ulong alternateId, ulong guildId)
        {
            var entity = await _context.GroupEntities.SingleAsync(x => x.AlternateKey == alternateId && x.GuildId == guildId);

            return entity.GroupId;
        }

        public async Task<int> GetGroupIdByName(string groupName)
        {
            var entity = await _context.GroupEntities.FirstOrDefaultAsync(x => x.GroupName == groupName);

            if(entity == null)
            {
                throw new ArgumentException("Sequence contains no element", groupName);
            }

            return entity.GroupId;
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