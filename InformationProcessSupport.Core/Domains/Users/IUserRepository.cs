namespace InformationProcessSupport.Core.Users
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity userEntity);
        Task AddCollectionAsync(List<UserEntity> userEntities);
        Task UpdateAsync(UserEntity userEntity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(ulong id, ulong guildId);
        Task<int> GetUserIdByAlternateId(ulong alternateId, ulong guildId);
        Task<ICollection<UserEntity>> GetUserCollectionAsync();
    }
}