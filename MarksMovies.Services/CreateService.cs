using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarksMovies.DataAccess;
using MarksMovies.Models;

namespace MarksMovies.Services
{
    public class CreateService : FetchImportService
    {
        private readonly IMovieDBAccess _dbAccess;

        public CreateService(IMovieDBAccess dbAccess, ITMDBapi TMDBapi) : base(TMDBapi)
        {
            _dbAccess = dbAccess;
        }



        public async Task<int> CreateAsync(Movie Movie, List<GenreType> SelectedGenres = null)
        {
            if (Movie != null)
            {
                // Always make new movies be ranked last
                Movie.Rank = _dbAccess.GetMovieCount() + 1;

                if (SelectedGenres != null)
                {
                    if (SelectedGenres.Count() > 0)
                    {
                        foreach (var item in SelectedGenres)
                        {
                            var g = new Genre(item);
                            Movie.Genres.Add(g);
                        }
                    }
                }

                _dbAccess.AddMovie(Movie);

                await _dbAccess.SaveChangesAsync();

                var movie = _dbAccess.LastOrDefault();

                return movie.ID;
            }
            else
                return 0;
        }
    }
}
