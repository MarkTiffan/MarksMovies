using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarksMovies.DataAccess;
using MarksMovies.Models;
using MarksMovies.TMDB;

namespace MarksMovies.Services
{
    public class CreateService : FetchImportService
    {
        private readonly IMovieDBAccess _dbAccess;

        public CreateService(IMovieDBAccess DbAccess, ITMDBapi TMDBapi) : base(TMDBapi)
        {
            _dbAccess = DbAccess;
        }



        public async Task<int> CreateAsync(Movie Movie)
        {
            if (Movie == null)
                return 0;

            // Always make new movies be ranked last
            Movie.Rank = _dbAccess.GetMovieCount() + 1;

            _dbAccess.AddMovie(Movie);

            await _dbAccess.SaveChangesAsync();

            var movie = _dbAccess.LastOrDefault();

            return movie.ID;
            
        }
    }
}
