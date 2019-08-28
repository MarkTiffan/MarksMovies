using MarksMovies.DataAccess;
using MarksMovies.Models;
using MarksMovies.TMDB;
using System.Threading.Tasks;

namespace MarksMovies.Services
{
    public class DetailsService
    {

        private readonly IMovieDBAccess _dbAccess;
        private readonly ITMDBapi _tmdbapi;



        public DetailsService(IMovieDBAccess dbAccess, ITMDBapi TMDBapi)
        {
            _dbAccess = dbAccess;
            _tmdbapi = TMDBapi;
        }

        public async Task<Movie> GetMovieAsync(int? Movie_ID)
        {
            return await _dbAccess.GetMovieAsync(Movie_ID);
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(int TMDBapi_ID)
        {
            return await _tmdbapi.FetchMovieDetailsAsync(TMDBapi_ID);
        }

        public async Task<TVShowDetails> GetTVSHowDetailsAsync(int TMDBapi_ID)
        {
            return await _tmdbapi.FetchTVShowDetailsAsync(TMDBapi_ID);
        }
    }
}
