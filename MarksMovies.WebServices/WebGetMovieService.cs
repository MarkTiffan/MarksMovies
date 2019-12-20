using MarksMovies.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarksMovies.WebServices
{
    public class WebGetMovieService
    {
        protected HttpClient _httpClient { get; }

        public WebGetMovieService(HttpClient HttpClient)
        {
            _httpClient = HttpClient;
        }

        public async Task<Movie> GetAsync(int? ID)
        {
            string requestUrl = "http://localhost:5000/api/media/item/";

            if (ID == null || ID <= 0)
                return null;

            requestUrl += ID;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return null;

            try
            {
                return JsonConvert.DeserializeObject<Movie>(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                return null;
            }
        }
    }
}
