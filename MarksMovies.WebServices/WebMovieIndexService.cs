using MarksMovies.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarksMovies.WebServices
{
    public class WebMovieIndexService
    {
        protected HttpClient Client { get; }

        public WebMovieIndexService(HttpClient HttpClient)
        {
            Client = HttpClient;
        }
        public async Task<IList<Movie>> OnGetAsync(string SearchTitle, GenreType SearchGenre)
        {
            string requestUrl = "http://localhost:5000/api/media/items";

            if (!string.IsNullOrEmpty(SearchTitle))
                requestUrl += "?Title=" + SearchTitle;

            if (SearchGenre != 0)
                if (requestUrl.Contains("?"))
                    requestUrl += "&Genre=" + SearchGenre;
                else
                    requestUrl += "?Genre=" + SearchGenre;

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);


            var response = await Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return null;

            try
            {
                return JsonConvert.DeserializeObject<IList<Movie>>(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                return null;
            }
        }
    }
}
