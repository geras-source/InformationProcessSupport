using InformationProcessSupport.Web.Dtos;
using InformationProcessSupport.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace InformationProcessSupport.Web.Shared
{
    public partial class Login
    {
        private UserAuthenticationDto _userForAuthentication = new UserAuthenticationDto();
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowAuthError { get; set; }
        public string Error { get; set; }
        public async Task ExecuteLogin()
        {
            ShowAuthError = false;
            var result = await AuthenticationService.Login(_userForAuthentication);
            if (!result.IsAuthSuccessful)
            {
                Error = result.ErrorMessage;
                ShowAuthError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
                StateHasChanged();
            }
        }
    }
}
