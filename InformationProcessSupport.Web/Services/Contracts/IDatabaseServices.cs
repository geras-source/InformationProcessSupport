using InformationProcessSupport.Web.Dtos;

namespace InformationProcessSupport.Web.Services.Contracts
{
    public interface IDatabaseServices
    {
        Task<IEnumerable<UsersDto>> GetUserCollectionAsync();
        Task<string> UpdateUserCollectionAsync(IEnumerable<UsersDto> users);
        Task<string> DeleteUserCollectionAsync(IEnumerable<UsersDto> users);
    }
}
