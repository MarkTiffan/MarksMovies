using MarksMovies.Models;

namespace MarksMoviesTests
{
    public class CommonTestFunctions
    {
        public CommonTestFunctions()
        {
        }
        public static Movie GetSampleMovie(bool WithGenres = false, int ID = 1)
        {
            Movie newMovie = new Movie
            {
                ID = ID,
                Title = "Avenger's Endgame",
                Year = 2019,
                Rating = Rating.PG13,
                MediaType = DiscType.UHD_Bluray,
                TMDB_ID = 299534,
                Rank = 1
            };
            if (WithGenres == true)
            {
                newMovie.Genres.Add(new Genre(GenreType.Action));
                newMovie.Genres.Add(new Genre(GenreType.Fantasy));
                newMovie.Genres.Add(new Genre(GenreType.SciFi));
            }
            return newMovie;
        }


        public static Movie GetSampleTVShow(bool WithGenres = false, int ID = 1)
        {
            Movie newMovie = new Movie
            {
                ID = ID,
                Title = "House",
                Year = 2004,
                Rating = Rating.TV_14,
                MediaType = DiscType.DVD,
                TMDB_ID = 1408,
                Rank = 2
            };
            if (WithGenres == true)
            {
                newMovie.Genres.Add(new Genre(GenreType.Drama));
                newMovie.Genres.Add(new Genre(GenreType.Comedy));
                newMovie.Genres.Add(new Genre(GenreType.Mystery));
            }
            return newMovie;
        }
    }
}
