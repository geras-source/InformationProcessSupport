using InformationProcessSupport.Web.Dtos;
using Microsoft.AspNetCore.Components;

namespace InformationProcessSupport.Web.Pages
{
    public class DisplayGroupFormBase : ComponentBase
    {
        [Parameter]
        public List<GroupDto> Groups { get; set; } = new()
        {
            new GroupDto
            {

            }
        };
    }
}
