using InformationProcessSupport.Data.Models;
using InformationProcessSupport.Server.Dtos;

namespace InformationProcessSupport.Server.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<UsersDto> ConvertToDto (this IEnumerable<UserModel> users)
        {
            return users.Select(it => new UsersDto
            {
                UserId = it.UserId,
                GroupName = it.GroupEntity?.GroupName,
                Name = it.Name,
                Roles = it.Roles,
                Nickname = it.Nickname
            }).ToList();
        }
    }
}
