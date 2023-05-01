using InformationProcessSupport.Web.Dtos;
using InformationProcessSupport.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace InformationProcessSupport.Web.Pages
{
    public class DisplayScheduleFormBase : ComponentBase
    {
        [Parameter] 
        public int CountForms { get; set; } = 1;
        [Parameter]
        public List<ScheduleDto> Schedules { get; set; } = new List<ScheduleDto>()
        {
            new ScheduleDto()
            {
                
            }
        };
        [Inject]
        private IScheduleServices ScheduleServices { get; set; }

        protected async Task AddScheduleCollection_Click()
        {
            await ScheduleServices.AddScheduleCollectionAsync(Schedules);
        }
    }
}
