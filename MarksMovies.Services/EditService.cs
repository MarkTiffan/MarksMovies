using MarksMovies.DataAccess;
using MarksMovies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarksMovies.Services
{
    public class EditService : FetchImportService
    {

        private readonly IMovieDBAccess _dbAccess;

        public EditService(IMovieDBAccess dbAccess, ITMDBapi TMDBapi) : base(TMDBapi)
        {
            _dbAccess = dbAccess;
        }


        public async Task<Movie> GetAsync(int? id)
        {
            return await _dbAccess.GetMovieAsync(id);
        }

        
        public async Task<int> SaveMovieAsync(Movie Movie, List<GenreType> SelectedGenres = null)
        {
            return await _dbAccess.SaveMovieAsync(Movie, SelectedGenres);
        }
    }
}
