using System.Net.Http;
using System.Threading.Tasks;

namespace MarksMovies.WebServices
{
    public class WebDeleteService : WebGetMovieService
    {
        public WebDeleteService(HttpClient HttpClient) : base(HttpClient)
        {

        }

        public const int DELETE_FAIL = 0;
        public const int DELETE_SUCCESS = 1;
        public async Task<int> DeleteMovieAsync(int? ID)
        {
            if (ID == null || ID <= 0)
                return 0;

            var requestUrl = "http://localhost:5000/api/media/item/" + ID;

            var response = await _httpClient.DeleteAsync(requestUrl);

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
