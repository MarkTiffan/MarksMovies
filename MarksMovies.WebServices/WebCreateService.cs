using MarksMovies.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MarksMovies.WebServices
{
    public class WebCreateService : WebFetchImportService
    {
        
        public WebCreateService(HttpClient HttpClient) : base(HttpClient)
        {
            
        }
        public async Task<int> CreateAsync(Movie Movie, IList<GenreType> SelectedGenres)
        {
            if (SelectedGenres?.Count() > 0)
            {
                foreach (var item in SelectedGenres)
                {
                    var g = new Genre(item);
                    Movie.Genres.Add(g);
                }
            }

            var requestUrl = "http://localhost:5000/api/media/item";

            var response = await _httpClient.PostAsync(requestUrl,
                new StringContent(JsonConvert.SerializeObject(Movie),
                    Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                return 0;

            try
            {
                int result;
                if (int.TryParse(await response.Content.ReadAsStringAsync(), out result))
                    return result;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
