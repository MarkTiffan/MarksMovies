using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarksMovies.WebServices
{
    public class WebExportService
    {
        protected HttpClient _httpClient;
        public WebExportService(HttpClient HttpClient)
        {
            _httpClient = HttpClient;
        }


        public async Task<string> GetMoviesJsonAsync()
        {
            string requestUrl = "http://localhost:5000/api/media/export/json";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return null;

            try
            {
                return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                return null;
            }
        }

        public async Task<byte[]> GetExcelFileAsync()
        {
            string requestUrl = "http://localhost:5000/api/media/export/excel";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return null;

            try
            {
                return JsonConvert.DeserializeObject<byte[]>(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                return null;
            }
        }
    }
}
