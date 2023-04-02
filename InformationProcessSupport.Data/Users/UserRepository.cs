using InformationProcessSupport.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Data.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public async Task AddAsync(UserEntity userEntity)
        {
            var entity = new UserModel
            {
                AlternateKey = userEntity.AlternateKey,
                Name = userEntity.Name,
                Nickname = userEntity.Nickname,
                Roles = userEntity.Roles,
                GuildId = userEntity.GuildId,
                GuildName= userEntity.GuildName
            };
            await _context.UserEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="guildId"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(ulong id, ulong guildId)
        {
            if (await _context.UserEntities.AnyAsync(x => x.AlternateKey == id && x.GuildId == guildId))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="alternateId"></param>
        /// <param name="guildId"></param>
        /// <returns></returns>
        public async Task<int> GetUserIdByAlternateId(ulong alternateId, ulong guildId)
        {
            var userId = await _context.UserEntities.SingleAsync(x => x.AlternateKey == alternateId && x.GuildId == guildId);

            return userId.UserId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="guildId"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.UserEntities.SingleOrDefaultAsync(x => x.UserId == id);
            
            if(entity != null)
            {
                _context.UserEntities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(UserEntity userEntity)
        {
            var entity = await _context.UserEntities.SingleAsync(x => x.UserId == userEntity.UserId);
            if (entity != null)
            {
                entity.Name = userEntity.Name;
                entity.Nickname = userEntity.Nickname;
                entity.Roles = userEntity.Roles;
                await _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntities"></param>
        /// <returns></returns>
        public async Task AddCollectionAsync(List<UserEntity> userEntities)
        {
            var entities = userEntities.Select(x => new UserModel
            {
                AlternateKey = x.AlternateKey,
                Name = x.Name,
                Nickname = x.Nickname,
                Roles = x.Roles,
                GuildId = x.GuildId,
                GuildName = x.GuildName,
                GroupId = x.GroupId
            });
            await _context.UserEntities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<UserEntity>> GetUserCollectionAsync()
        {
            var entities = await _context.UserEntities.Select(it => new UserEntity
            {
                UserId = it.UserId,
                Name = it.Name,
                Nickname = it.Nickname,
                GroupId = it.GroupId
            }).ToListAsync();
            return entities;
        }
    }
}