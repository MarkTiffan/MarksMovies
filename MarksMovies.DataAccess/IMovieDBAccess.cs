using MarksMovies.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarksMovies.DataAccess
{
    public interface IMovieDBAccess
    {
        int GetMovieCount();

        Movie LastOrDefault();

        void AddMovie(Movie Movie);

        Task<int> SaveChangesAsync();

        Task<Movie> GetMovieAsync(int? id);

        Task<int> DeleteMovieAsync(int? id);

        Task<int> SaveMovieAsync(Movie Movie, List<GenreType> SelectedGenres);

        bool MovieExists(int id);

        Task<IList<Movie>> GetMovieListAsync(string SearchTitle, GenreType SearchGenre);

        IList<Movie> GetRankedMovies();

        void Update(Movie Movie);

        IQueryable<Movie> GetMovieList();
    }
}
