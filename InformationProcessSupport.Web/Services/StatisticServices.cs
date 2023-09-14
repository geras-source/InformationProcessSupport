using System.Net.Http.Json;
using InformationProcessSupport.Web.Dtos;
using InformationProcessSupport.Web.Services.Contracts;

namespace InformationProcessSupport.Web.Services
{
    public class StatisticServices : IStatisticServices
    {
        private readonly HttpClient _httpClient;

        public StatisticServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<StatisticDto.StatisticByGroup>> GetStatisticByGroupCollectionAsync()
        {
            var response = await _httpClient.GetAsync("api/Statistic/GetStatisticsByGroup");

            return await response.Content.ReadFromJsonAsync<IEnumerable<StatisticDto.StatisticByGroup>>();
        }

        public async Task<IEnumerable<StatisticDto.StatisticByUser>> GetStatisticByUserAsync(string userName)
        {
            var response = await _httpClient.GetAsync($"api/Statistic/GetStatisticByUser/{userName}");

            return await response.Content.ReadFromJsonAsync<IEnumerable<StatisticDto.StatisticByUser>>();
        }
    }
}
