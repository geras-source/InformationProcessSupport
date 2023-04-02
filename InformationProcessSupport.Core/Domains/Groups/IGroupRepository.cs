namespace InformationProcessSupport.Core.Groups
{
    public interface IGroupRepository
    {
        Task AddCollectionGroupAsync(List<GroupEntity> groupEntities);
        Task<int> GetGroupIdByAlternateId (ulong alternateId, ulong guildId);
        Task<int> GetGroupIdByName (string groupName);
        Task AddAsync(GroupEntity groupEntity);
        Task UpdateAsync (GroupEntity groupEntity);
        Task DeleteAsync(int groupId);
        Task<ICollection<GroupEntity>> GetGroupCollectionAsync();
    }
}
