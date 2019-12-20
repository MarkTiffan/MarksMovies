using MarksMovies.Models;
using MarksMovies.TMDB;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarksMovies.WebServices
{
    public class WebFetchImportService : WebGetMovieService
    {
        
        public WebFetchImportService(HttpClient HttpClient) : base(HttpClient)
        {
            
        }
        public virtual async Task<SearchMovies> FetchMovieAsync(string Title)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://localhost:5000/api/movie/fetch?title=" +
                Title
            );

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return null;

            try
            {
                return JsonConvert.DeserializeObject<SearchMovies>(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                return null;
            }
        }


        public virtual async Task<SearchTV> FetchTVShowsAsync(string Title)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://localhost:5000/api/tvshow/fetch?title=" +
                Title
            );
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return null;

            try
            {
                return JsonConvert.DeserializeObject<SearchTV>(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                return null;
            }
        }


        public virtual async Task<Movie> ImportMovieAsync(int TMDB_ID, string Title, Movie Movie)
        {
            var requestUrl = "http://localhost:5000/api/Movie/import?TMDB_ID=" + 
                TMDB_ID + "&" +
                "title=" + Title;
            
            var response = await _httpClient.PostAsync(requestUrl,
                new StringContent(JsonConvert.SerializeObject(Movie), 
                    Encoding.UTF8, "application/json"));
            

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


        public virtual async Task<Movie> ImportTVShowAsync(int TMDB_ID, string Title, Movie Movie)
        {
            var requestUrl = "http://localhost:5000/api/tvshow/import?TMDB_ID=" +
                TMDB_ID + "&" +
                "title=" + Title;

            var response = await _httpClient.PostAsync(requestUrl,
                new StringContent(JsonConvert.SerializeObject(Movie),
                    Encoding.UTF8, "application/json"));


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
