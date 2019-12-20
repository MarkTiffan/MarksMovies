using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace MarksMovies.TMDB
{

    public class TMDBapi : ITMDBapi
    { 
        // hard-coding this api key for now
        private const string api_key = "4e1d0b30dd7aa6df08b8060f7ae82800";
    
        private HttpClient Client { get; }

        public TMDBapi()
        {
        }

        public TMDBapi(HttpClient HttpClient)
        {
            Client = HttpClient;
        }


        //  Fetch movies from TMDB based on a keyword search
        public async Task<SearchMovies> FetchMovieAsync(string title)
        {
            if (string.IsNullOrEmpty(title))
                return null;
            
            var request = new HttpRequestMessage(HttpMethod.Get,
                        "https://api.themoviedb.org/3/search/movie?api_key=" +
                        api_key +
                        "&language=en-US&query=" +
                        title +
                        "&page=1&include_adult=false");

            var response = await Client.SendAsync(request);

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


        // Fetch the details of a specific movie from TMDB based on the TMDB_ID
        public async Task<MovieDetails> FetchMovieDetailsAsync(int TMDB_ID)
        {
            if (TMDB_ID <= 0)
                return null;
            
            var request = new HttpRequestMessage(HttpMethod.Get,
                        "https://api.themoviedb.org/3/movie/" +
                        TMDB_ID +
                        "?api_key=" +
                        api_key +
                        "&language=en-US&" +
                        "&page=1&include_adult=false&append_to_response=release_dates,credits");

            var response = await Client.SendAsync(request);

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



        //  Fetch TV Shows from TMDB based on a keyword search
        public async Task<SearchTV> FetchTVShowsAsync(string title)
        {
            if (string.IsNullOrEmpty(title))
                return null;
            
            var request = new HttpRequestMessage(HttpMethod.Get,
                        "https://api.themoviedb.org/3/search/tv?api_key=" +
                        api_key +
                        "&language=en-US&query=" +
                        title +
                        "&page=1");

            var response = await Client.SendAsync(request);

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


        // Fetch the details of a specific TV Show from TMDB based on the TMDB_ID
        public async Task<TVShowDetails> FetchTVShowDetailsAsync(int TMDB_ID)
        {
            if (TMDB_ID <= 0)
                return null;
            
            var request = new HttpRequestMessage(HttpMethod.Get,
                        "https://api.themoviedb.org/3/tv/" +
                        TMDB_ID +
                        "?api_key=" +
                        api_key +
                        "&language=en-US" +
                        "&append_to_response=credits,content_ratings");

            var response = await Client.SendAsync(request);

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
