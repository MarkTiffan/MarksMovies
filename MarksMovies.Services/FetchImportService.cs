using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarksMovies.Models;
using MarksMovies.TMDB;

namespace MarksMovies.Services
{
    public class FetchImportService
    {
        private ITMDBapi _TMDBapi { get; set; }

        public FetchImportService(ITMDBapi tmdbapi)
        {
            _TMDBapi = tmdbapi;
        }
        public virtual async Task<SearchMovies> FetchMovieAsync(string Title)
        {
            if (Title == string.Empty)
                return null;

            return await _TMDBapi.FetchMovieAsync(Title);
        }


        public virtual async Task<SearchTV> FetchTVShowsAsync(string Title)
        {
            if (Title == string.Empty)
                return null;

            return await _TMDBapi.FetchTVShowsAsync(Title);
        }


        public virtual async Task<Movie> ImportMovieAsync(int TMDB_ID)
        {
            var Movie = new Movie();

            if (TMDB_ID <= 0)
                return Movie;

            MovieDetails movieDetails = await _TMDBapi.FetchMovieDetailsAsync(TMDB_ID);

            if (movieDetails == null)
                return Movie;

            Movie.TMDB_ID = movieDetails.id;
            Movie.Title = movieDetails.title;

            Movie.Year = Convert.ToDateTime(movieDetails.release_date).Year;

            Movie.Genres = new List<Genre>();
            movieDetails?.genres.ToList()
                            .ForEach(genre => Movie.Genres.Add(new Genre(genre.id)));
                    

            if (Movie.TMDB_ID <= 0)
                return Movie;

            
            Movie.IMDB_ID = movieDetails.imdb_id;

            var ratingFound = false;
            foreach (var releasedate in movieDetails.release_dates?.results)
            {
                if ((releasedate.iso_3166_1 != "US") || (releasedate.release_dates == null))
                    continue;
                
                foreach (var rd in releasedate.release_dates)
                {
                    if (rd.type == ReleaseDateType.Premiere || rd.type == ReleaseDateType.Theatrical)
                    {
                        Movie.Rating = Movie.RatingFromString(rd.certification);
                        ratingFound = true;
                        break;
                    }
                }
                if (ratingFound == true)
                    break;
            }

            return Movie;
        }




        public virtual async Task<Movie> ImportTVShowAsync(int TMDB_ID)
        {
            var Movie = new Movie();

            if (TMDB_ID <= 0)
                return Movie;


            TVShowDetails TVShowDetails = await _TMDBapi.FetchTVShowDetailsAsync(TMDB_ID);

            if (TVShowDetails == null)
                return Movie;


                
            Movie.TMDB_ID = TVShowDetails.id;
            Movie.Title = TVShowDetails.name;

            Movie.Genres = new List<Genre>();

            TVShowDetails?.genres.ToList()
                            .ForEach(genre => Movie.Genres.Add(new Genre(genre.id)));



            if (Movie.TMDB_ID <= 0)
                return Movie;

            
            Movie.IMDB_ID = string.Empty;
            Movie.Year = Convert.ToDateTime(TVShowDetails.first_air_date).Year;

            if (TVShowDetails.seasons?.Count() > 0 && Movie.Season != 0)
            {
                foreach (var season in TVShowDetails.seasons)
                {
                    if (season.season_number == Movie.Season)
                    {
                        //overwrite the year if was have a value from the seasons collection
                        Movie.Year = Convert.ToDateTime(season.air_date).Year;
                        break;
                    }
                }
            }

            foreach (var rating in TVShowDetails.content_ratings?.results)
            {
                if (rating.iso_3166_1 == "US")
                {
                    Movie.Rating = Movie.RatingFromString(rating.rating);
                    break;            
                }
            }

            return Movie;
        }
    }
}
