using InformationProcessSupport.Web.Dtos;
using InformationProcessSupport.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace InformationProcessSupport.Web.Pages
{
    public class DisplayUserTableBase : ComponentBase
    {
        public IEnumerable<UsersDto> Users { get; set; }
        [Inject] public IDatabaseServices DatabaseServices { get; set; }
        [Inject] public IModalService ModalService { get; set; }
        internal bool IsEditMode { get; set; }
        private string? _response { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Users = await DatabaseServices.GetUserCollectionAsync();
        }

        internal async Task<string> UpdateUsersAsync(IEnumerable<UsersDto> users)
        {
            return await DatabaseServices.UpdateUserCollectionAsync(users);
        }

        internal async Task<string> DeleteUserCollectionAsync(IEnumerable<UsersDto> users)
        {
            return await DatabaseServices.DeleteUserCollectionAsync(users);
        }

        internal void StartEditingMode()
        {
            IsEditMode = true;
        }

        internal async Task DeleteSelectedFields()
        {
            var deletedUsers = Users.Where(user => user.IsSelected);
            _response = await DeleteUserCollectionAsync(deletedUsers);
            DisplayModalWindow("Удаление данных", _response);
            IsEditMode = false;
            Users = await DatabaseServices.GetUserCollectionAsync();
        }

        internal async Task UpdateEditingFields()
        {
            var changedUsers = Users.Where(x => x.IsModified);
            _response = await UpdateUsersAsync(changedUsers);
            DisplayModalWindow("Обновление данных", _response);
            IsEditMode = false;
        }

        private void DisplayModalWindow(string title, string message)
        {
            var parameters = new ModalParameters
            {
                { nameof(Confirm.Message), message }
            };

            ModalService.Show<Confirm>(title, parameters);
        }
    }
}