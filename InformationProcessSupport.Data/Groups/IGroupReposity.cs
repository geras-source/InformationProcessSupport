using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProcessSupport.Data.Groups
{
    public interface IGroupReposity
    {
        Task AddRangeGroups(List<GroupEntity> groupEntities);
        Task<int> GetGroupIdByAlternateId (ulong alternateId, ulong guildId);
        Task AddAsync(GroupEntity groupEntity);
        Task UpdateAsync (GroupEntity groupEntity);
        Task DeleteAsync(int groupId);
    }
}
