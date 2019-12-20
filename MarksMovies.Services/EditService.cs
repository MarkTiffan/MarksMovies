using MarksMovies.DataAccess;
using MarksMovies.Models;
using MarksMovies.TMDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarksMovies.Services
{
    public class EditService : FetchImportService
    {

        private readonly IMovieDBAccess _dbAccess;

        public EditService(IMovieDBAccess DbAccess, ITMDBapi TMDBapi) : base(TMDBapi)
        {
            _dbAccess = DbAccess;
        }


        
        public async Task<int> SaveMovieAsync(Movie Movie, IList<GenreType> SelectedGenres = null)
        {
            return await _dbAccess.SaveMovieAsync(Movie, SelectedGenres);
        }
    }
}
