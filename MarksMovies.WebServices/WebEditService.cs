using MarksMovies.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MarksMovies.WebServices
{
    public class WebEditService : WebFetchImportService
    {
        public WebEditService(HttpClient HttpClient) : base(HttpClient)
        {
            
        }


        public async Task<int> SaveMovieAsync(Movie Movie, IList<GenreType> SelectedGenres = null)
        {
            if (Movie == null)
                return 0;

            if (SelectedGenres?.Count() > 0)
            {
                foreach (var item in SelectedGenres)
                {
                    var g = new Genre(item);
                    Movie.Genres.Add(g);
                }
            }

            var requestUrl = "http://localhost:5000/api/media/item/" + Movie.ID;

            var response = await _httpClient.PutAsync(requestUrl,
                new StringContent(JsonConvert.SerializeObject(Movie),
                    Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                return 0;

            try
            {
                if (int.TryParse(await response.Content.ReadAsStringAsync(), out int result))
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
