using Microsoft.AspNetCore.Components;

namespace InformationProcessSupport.Web.Shared
{
    public partial class ModalWindow : ComponentBase, IDisposable
    {
        [Parameter] 
        public string Header { get; set; }

        public void Dispose()
        {
            StateHasChanged();
            GC.SuppressFinalize(this);
        }
    }
}
