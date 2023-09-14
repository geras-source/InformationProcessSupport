using InformationProcessSupport.Web.Dtos;
using InformationProcessSupport.Web.Services;
using InformationProcessSupport.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace InformationProcessSupport.Web.Pages
{
    public partial class StatisticByUser
    {
        public IEnumerable<StatisticDto.StatisticByUser> StatisticsByUser { get; set; }
        [Inject] IStatisticServices StatisticServices { get; set; }
        public string UserName { get; set; }

        internal void RefreshUserStatistic()
        {
            StatisticsByUser = null;
            UserName = null;
        }

        internal async Task GetUserStatistic(string userName)
        {
            StatisticsByUser = await StatisticServices.GetStatisticByUserAsync(userName);
        }
    }
}
