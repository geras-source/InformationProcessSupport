using System.Net.Http.Json;
using InformationProcessSupport.Web.Dtos;
using InformationProcessSupport.Web.Services.Contracts;

namespace InformationProcessSupport.Web.Services
{
    public class ScheduleServices : IScheduleServices
    {
        private readonly HttpClient _httpClient;

        public ScheduleServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task AddScheduleCollectionAsync(ICollection<ScheduleDto> schedules)
        {
            var response = await _httpClient.PostAsJsonAsync<ICollection<ScheduleDto>>("api/Schedule/PostScheduleCollection", schedules);
            //TODO: проверка ответа
        }
    }
}
