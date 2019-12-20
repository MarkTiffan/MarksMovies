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


        public virtual async Task<Movie> ImportMovieAsync(int TMDB_ID, string Title, Movie Movie = null)
        {
            if(Movie == null)
                Movie = new Movie();

            if (TMDB_ID <= 0 || string.IsNullOrEmpty(Title))
                return Movie;

            SearchMovies searchMovies = await _TMDBapi.FetchMovieAsync(Title);

            foreach (var result in searchMovies?.results)
            {
                if (result.id == TMDB_ID)
                {
                    Movie.TMDB_ID = result.id;
                    Movie.Title = result.title;

                    Movie.Year = Convert.ToDateTime(result.release_date).Year;

                    Movie.Genres = new List<Genre>();
                    result?.genre_ids.ToList()
                                    .ForEach(genre => Movie.Genres.Add(new Genre(genre)));
                    
                    break;
                }
            }

            if (Movie.TMDB_ID <= 0)
                return Movie;

            MovieDetails movieDetails = await _TMDBapi.FetchMovieDetailsAsync(Movie.TMDB_ID);

            if (movieDetails == null)
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




        public virtual async Task<Movie> ImportTVShowAsync(int TMDB_ID, string Title, Movie Movie = null)
        {
            if (Movie == null)
                Movie = new Movie();

            if (TMDB_ID <= 0 || string.IsNullOrEmpty(Title))
                return Movie;
            
            SearchTV SearchTVShow = await _TMDBapi.FetchTVShowsAsync(Title);

            if (SearchTVShow == null)
                return Movie;
            
            foreach (var result in SearchTVShow.results)
            {
                if (result.id != TMDB_ID)
                    continue;
                
                Movie.TMDB_ID = result.id;
                Movie.Title = result.name;

                Movie.Genres = new List<Genre>();

                result?.genre_ids.ToList()
                                .ForEach(genre => Movie.Genres.Add(new Genre(genre)));
                break;  
            }

            if (Movie.TMDB_ID <= 0)
                return Movie;
            
            TVShowDetails TVShowDetails = await _TMDBapi.FetchTVShowDetailsAsync(Movie.TMDB_ID);

            if (TVShowDetails == null)
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
