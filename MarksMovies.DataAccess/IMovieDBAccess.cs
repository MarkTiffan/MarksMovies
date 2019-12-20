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

        Task<Movie> GetMovieAsync(int? ID);

        Task<int> DeleteMovieAsync(int? ID);

        Task<int> SaveMovieAsync(Movie Movie, IList<GenreType> SelectedGenres);

        bool MovieExists(int ID);

        Task<IList<Movie>> GetMovieListAsync(string SearchTitle, GenreType SearchGenre);

        Task<IList<Movie>> GetMovieListAsync();

        IList<Movie> GetRankedMovies();

        void Update(Movie Movie);


    }
}
