using InformationProcessSupport.Website.Pages;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace InformationProcessSupport.Website.Services
{
    public class HttpService
    {
        private static HttpClient _httpClient = new HttpClient();
        /// <summary>
        /// Конструктор задает базовый адрес универсального кода ресурса
        /// </summary>
        /// <param name="configuration">Файл конфигурации</param>
        public HttpService(/*IConfiguration configuration*/)
        {
            //_httpClient.BaseAddress = new Uri("localhost:7099"/*configuration["ConnectionStrings:ConnectingToServer"]*/);
        }
        internal async Task PostSchedule(ScheduleEntity scheduleEntity)
        {
            //var response = new HttpResponseMessage();
            var entity = new ScheduleEntity
            {
                SubjectName = scheduleEntity.SubjectName,
                StartTimeTheSubject = scheduleEntity.StartTimeTheSubject
            };
            var json = JsonConvert.SerializeObject(scheduleEntity);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
               var response = await _httpClient.PostAsync("https://localhost:7099/WeatherForecast/GetWeatherForecast", data);
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// Получить статус сканирования c помощью идентификатора
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Отчет</returns>
        internal async Task<string> GetStatusScan()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7099/WeatherForecast/GetWeatherForecast");
            }
            catch ( Exception ex )
            {
                Console.WriteLine(ex.ToString());
            }
          
            
            
            Console.WriteLine("");
            return "";
        }

        /// <summary>
        /// Конвертировать файлы и отправить их на сканирование
        /// </summary>
        /// <param name="pathDirectory">Путь директории для сканирования</param>
        /// <returns>Идентификатор сканирования</returns>
        internal async Task<string> SendFilesToScan(string pathDirectory)
        {
            var filesPath = Directory.GetFiles(pathDirectory, "*", SearchOption.AllDirectories).ToList();
            try
            {
                var files = ConvertToFormFile(filesPath);

                var content = new MultipartFormDataContent();
                foreach (var file in files)
                {
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("application/json")
                    {
                        Name = "formFiles",
                        FileName = file.FileName
                    };
                    content.Add(fileContent);
                }
                content.Add(new StringContent(pathDirectory), "pathDirectory");
                var response = await _httpClient.PostAsync(_httpClient.BaseAddress, content);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return "Exception";
            }

        }

        /// <summary>
        /// Конвертор файлов для отправки в запросе
        /// </summary>
        /// <param name="filesPath">Список файлов</param>
        /// <returns>Список файлов подготовленных для отправки</returns>
        private List<FormFile> ConvertToFormFile(List<string> filesPath)
        {
            var result = new List<FormFile>();
            foreach (var filePath in filesPath)
            {
                var stream = new MemoryStream(File.ReadAllBytes(filePath).ToArray());
                var file = new FormFile(stream, 0, stream.Length, filePath, Path.GetFileName(filePath));
                result.Add(file);
            }

            return result;
        }
    }
}
