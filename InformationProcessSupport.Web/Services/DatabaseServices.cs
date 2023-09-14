using System.Net;
using System.Net.Http.Json;
using InformationProcessSupport.Web.Dtos;
using InformationProcessSupport.Web.Services.Contracts;

namespace InformationProcessSupport.Web.Services
{
    public class DatabaseServices : IDatabaseServices
    {
        private readonly HttpClient _httpClient;

        public DatabaseServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<UsersDto>> GetUserCollectionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Database/GetUserCollection");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<UsersDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<UsersDto>>();
                }

                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status code: {response.StatusCode} message: {message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<string> UpdateUserCollectionAsync(IEnumerable<UsersDto> users)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("api/Database/UpdateUsers", users);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }

                    return await response.Content.ReadAsStringAsync();
                }

                var message = await response.Content.ReadAsStringAsync();
                return message;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> DeleteUserCollectionAsync(IEnumerable<UsersDto> users)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Database/DeleteUsers", users);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {

            }

            return default;
        }
    }
}
