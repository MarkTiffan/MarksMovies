using MarksMovies.TMDB;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarksMovies.WebServices
{
    public class WebDetailsService : WebGetMovieService
    {
        
        public WebDetailsService(HttpClient HttpClient) : base(HttpClient)
        {
            
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(int TMDBapi_ID)
        {
            string requestUrl = "http://localhost:5000/api/movie/details/";

            if (TMDBapi_ID <= 0)
                return null;

            requestUrl += TMDBapi_ID;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return null;

            try
            {
                return JsonConvert.DeserializeObject<MovieDetails>(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                return null;
            }
        }

        public async Task<TVShowDetails> GetTVSHowDetailsAsync(int TMDBapi_ID)
        {
            string requestUrl = "http://localhost:5000/api/tvshow/details/";

            if (TMDBapi_ID <= 0)
                return null;

            requestUrl += TMDBapi_ID;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return null;

            try
            {
                return JsonConvert.DeserializeObject<TVShowDetails>(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                return null;
            }
        }
    }
}
