using MarksMovies.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarksMovies.WebServices
{
    public class WebRankMovieService
    {
        protected HttpClient _httpClient;

        public WebRankMovieService(HttpClient HttpClient)
        {
            _httpClient = HttpClient;
        }

        public async Task<IList<Movie>> GetRankedMoviesAsync()
        {
            string requestUrl = "http://localhost:5000/api/media/rankedlist";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);


            var response = await _httpClient.SendAsync(request);

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


        public async Task<int> UpdateRanksAsync(string itemIds)
        {
            string requestUrl = "http://localhost:5000/api/media/rankedlist";

            var response = await _httpClient.PostAsync(requestUrl,
                new StringContent(JsonConvert.SerializeObject(itemIds),
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
