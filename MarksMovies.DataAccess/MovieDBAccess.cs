using MarksMovies.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarksMovies.DataAccess
{
    public class MovieDBAccess : IMovieDBAccess
    {
        private readonly IMarksMoviesContext _context;

        public MovieDBAccess(IMarksMoviesContext context)
        {
            _context = context;
        }

        public const int DELETE_FAIL = 0;
        public const int DELETE_OK = 1;

        public void AddMovie(Movie Movie)
        {
            _context.Movie.Add(Movie);
        }

        public async Task<int> DeleteMovieAsync(int? id)
        {
            Movie Movie = await GetMovieAsync(id);

            if (Movie == null)
            {
                return DELETE_FAIL;
            }
            else
            {
                if (Movie.Genres != null)
                {
                    foreach (var genre in Movie.Genres)
                    {
                        _context.Remove(genre);
                    }
                }
                _context.Movie.Remove(Movie);
                await _context.SaveChangesAsync();

                return DELETE_OK;
            }
        }

        public async Task<Movie> GetMovieAsync(int? id)
        {
            if (id != null && id > 0)
                return await _context.Movie.Include(g => g.Genres).FirstOrDefaultAsync(m => m.ID == id);
            else
                return null;
        }

        public int GetMovieCount()
        {
            return _context.Movie.Count();
        }


        public Movie LastOrDefault()
        {
            return _context.Movie.LastOrDefault();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SaveMovieAsync(Movie Movie, List<GenreType> SelectedGenres = null)
        {
            if (Movie != null)
            {
                var movie = await GetMovieAsync(Movie.ID);

                _context.Entry(movie).CurrentValues.SetValues(Movie);

                if (SelectedGenres != null)
                {
                    if (movie.Genres.Count > 0)
                        movie.Genres.Clear();

                    if (SelectedGenres.Count > 0)
                    {
                        foreach (var moviegenre in SelectedGenres)
                        {
                            movie.Genres.Add(new Genre(moviegenre));
                        }
                    }
                }

                try
                {
                    await _context.SaveChangesAsync();
                    return 1;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (MovieExists(Movie.ID))
                        throw;
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }

        public bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }

        public async Task<IList<Movie>> GetMovieListAsync(string SearchTitle, GenreType SearchGenre)
        {
            IQueryable<Movie> movies = (from m in _context.Movie
                                        orderby m.Title, m.Season
                                        select m).Include("Genres");

            if (!string.IsNullOrEmpty(SearchTitle))
            {
                movies = movies.Where(s => s.Title.ToLower().Contains(SearchTitle.ToLower()));
            }

            if (SearchGenre != 0)
            {
                var g = new Genre(SearchGenre);
                movies = movies.Where(x => x.Genres.Any(y => y.genre == g.genre));
            }

            IList<Movie> movie = await movies.ToListAsync();

            return movie.OrderBy(s =>
                    (s.Title.StartsWith("A ", StringComparison.OrdinalIgnoreCase) || s.Title.StartsWith("The ", StringComparison.OrdinalIgnoreCase)) ?
                    s.Title.Substring(s.Title.IndexOf(" ") + 1) :
                    s.Title).ToList();
        }


        public IList<Movie> GetRankedMovies()
        {
            var movies = (from m in _context.Movie
                          where m.MovieOrTVShow == MovieOrTVShow.Movie
                          orderby m.Rank, m.Title
                          select m).Include("Genres");

            return movies.ToList();
        }

        public void Update(Movie Movie)
        {
            _context.Update(Movie);
        }


        public IQueryable<Movie> GetMovieList()
        {
            return (from m in _context.Movie
                    orderby m.Title, m.Season
                    select m).Include("Genres");
        }
    }
}
