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
        public async Task AddAsync(UserEntity userEntity)
        {
            var entity = new UserEntity
            {
                AlternateKey = userEntity.AlternateKey,
                Name = userEntity.Name,
                Nickname = userEntity.Nickname,
                Roles = userEntity.Roles
            };
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(ulong id, string guildName)
        {
            if (await _context.Users.AnyAsync(x => x.AlternateKey == id && x.GuildName == guildName))
            {
                return true;
            }
            return false;
        }
        public async Task<ulong> GetUserIdByAlternateId(ulong alternateId, string guildName)
        {
            var userId = await _context.Users.SingleAsync(x => x.AlternateKey == alternateId && x.GuildName == guildName);

            return userId.UserId;
        }
        public Task DeleteAsync(ulong id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public async Task AddCollectionAsync(List<UserEntity> userEntities)
        {
            await _context.Users.AddRangeAsync(userEntities);
            await _context.SaveChangesAsync();
        }
        //TODO: добавить метод, который добавляет сразу всю коллекцию в контекст
    }
}