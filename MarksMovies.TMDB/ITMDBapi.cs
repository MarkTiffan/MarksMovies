
using System.Threading.Tasks;

namespace MarksMovies
{
    public interface ITMDBapi
    {
        Task<SearchMovies> FetchMovieAsync(string title);
        Task<MovieDetails> FetchMovieDetailsAsync(int TMDB_ID);
        Task<SearchTV> FetchTVShowsAsync(string title);
        Task<TVShowDetails> FetchTVShowDetailsAsync(int TMDB_ID);
    }
}
