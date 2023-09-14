using InformationProcessSupport.Web.Dtos;

namespace InformationProcessSupport.Web.Services.Contracts
{
    public interface IAuthenticationService
    {
        //Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<AuthResponseDto> Login(UserAuthenticationDto userForAuthentication);
        Task Logout();
    }
}
