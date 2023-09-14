using InformationProcessSupport.Web.Dtos;
using InformationProcessSupport.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace InformationProcessSupport.Web.Pages
{
    public class StatisticBase : ComponentBase
    {
        [Inject] public IModalService ModalService { get; set; }
        [Inject] public HttpClient httpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            
            
        }
    }
}
