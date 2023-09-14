using InformationProcessSupport.Web.Dtos;
using InformationProcessSupport.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace InformationProcessSupport.Web.Pages
{
    public partial class StatisticByGroup
    {
        public IEnumerable<StatisticDto.StatisticByGroup> StatisticsByGroups { get; set; }
        [Inject] IStatisticServices StatisticServices { get; set; }
        protected override async Task OnInitializedAsync()
        {
            StatisticsByGroups = await StatisticServices.GetStatisticByGroupCollectionAsync();
        }
    }
}
