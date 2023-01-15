namespace InformationProcessSupport.Data.Users
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity userEntity);
        Task AddCollectionAsync(List<UserEntity> userEntities);
        Task UpdateAsync(UserEntity userEntity);
        Task DeleteAsync(ulong id);
        Task<bool> ExistsAsync(ulong id, string guildName);
        Task<ulong> GetUserIdByAlternateId(ulong alternateId, string guildName);
    }
}