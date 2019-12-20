using MarksMovies.DataAccess;
using MarksMovies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarksMovies.Services
{
    public class MovieIndexService
    {

        private readonly IMovieDBAccess _dbAccess;

        public MovieIndexService(IMovieDBAccess DbAccess)
        {
            _dbAccess = DbAccess;
        }



        public async Task<IList<Movie>> OnGetAsync(string SearchTitle, GenreType SearchGenre)
        {
            return await _dbAccess.GetMovieListAsync(SearchTitle, SearchGenre);
        }

    }
}
