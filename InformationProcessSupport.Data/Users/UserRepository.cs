using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata;

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
            var entity = new UserEntity
            {
                AlternateKey = userEntity.AlternateKey,
                Name = userEntity.Name,
                Nickname = userEntity.Nickname,
                Roles = userEntity.Roles,
                GuildId = userEntity.GuildId,
                GuildName= userEntity.GuildName
            };
            await _context.UsersEntity.AddAsync(entity);
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
            if (await _context.UsersEntity.AnyAsync(x => x.AlternateKey == id && x.GuildId == guildId))
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
            var userId = await _context.UsersEntity.SingleAsync(x => x.AlternateKey == alternateId && x.GuildId == guildId);

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
            var entity = await _context.UsersEntity.SingleOrDefaultAsync(x => x.UserId == id);
            
            if(entity != null)
            {
                _context.UsersEntity.Remove(entity);
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
            var entity = await _context.UsersEntity.SingleAsync(x => x.UserId == userEntity.UserId);
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
            await _context.UsersEntity.AddRangeAsync(userEntities);
            await _context.SaveChangesAsync();
        }
    }
}