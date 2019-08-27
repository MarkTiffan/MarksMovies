using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarksMovies.Models;

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
            if (Title != "")
                return await _TMDBapi.FetchMovieAsync(Title);
            else
                return null;
        }


        public virtual async Task<SearchTV> FetchTVShowsAsync(string Title)
        {
            if (Title != "")
                return await _TMDBapi.FetchTVShowsAsync(Title);
            else
                return null;
        }


        public virtual async Task<Movie> ImportMovieAsync(int TMDB_ID, string Title, Movie Movie = null)
        {
            SearchMovies SearchMovies;
            MovieDetails MovieDetails;
            if(Movie == null)
                Movie = new Movie();

            if (TMDB_ID > 0 && (!string.IsNullOrEmpty(Title)))
            {
                SearchMovies = await _TMDBapi.FetchMovieAsync(Title);

                if (SearchMovies != null)
                {
                    for (var i = 0; i < SearchMovies.results.Count(); i++)
                    {
                        if (SearchMovies.results[i].id == TMDB_ID)
                        {
                            Movie.TMDB_ID = SearchMovies.results[i].id;
                            Movie.Title = SearchMovies.results[i].title;

                            Movie.Year = Convert.ToDateTime(SearchMovies.results[i].release_date).Year;

                            if (SearchMovies.results[i].genre_ids.Count > 0)
                            {
                                Movie.Genres = new List<Genre>();
                                foreach (var genre in SearchMovies.results[i].genre_ids)
                                {
                                    Movie.Genres.Add(new Genre(genre));
                                }
                            }
                            break;
                        }
                    }
                }

                if (Movie.TMDB_ID > 0)
                {
                    MovieDetails = await _TMDBapi.FetchMovieDetailsAsync(Movie.TMDB_ID);

                    if (MovieDetails != null)
                    {
                        Movie.IMDB_ID = MovieDetails.imdb_id;

                        if (MovieDetails.release_dates != null)
                        {
                            if (MovieDetails.release_dates.results != null)
                            {
                                foreach (var releasedate in MovieDetails.release_dates.results)
                                {
                                    if (releasedate.iso_3166_1 == "US")
                                    {
                                        if (releasedate.release_dates != null)
                                        {
                                            foreach (var rd in releasedate.release_dates)
                                            {
                                                if (rd.type == ReleaseDateType.Premiere)
                                                {
                                                    Movie.Rating = Movie.RatingFromString(rd.certification);
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }

                }
            }
            return Movie;

        }




        public virtual async Task<Movie> ImportTVShowAsync(int TMDB_ID, string Title, Movie Movie = null)
        {
            SearchTV SearchTVShow;
            TVShowDetails TVShowDetails;
            if (Movie == null)
                Movie = new Movie();

            if (TMDB_ID > 0 && (!string.IsNullOrEmpty(Title)))
            {
                SearchTVShow = await _TMDBapi.FetchTVShowsAsync(Title);

                if (SearchTVShow != null)
                {
                    for (var i = 0; i < SearchTVShow.results.Count(); i++)
                    {
                        if (SearchTVShow.results[i].id == TMDB_ID)
                        {
                            Movie.TMDB_ID = SearchTVShow.results[i].id;
                            Movie.Title = SearchTVShow.results[i].name;

                            if (SearchTVShow.results[i].genre_ids.Count > 0)
                            {
                                Movie.Genres = new List<Genre>();
                                foreach (var genre in SearchTVShow.results[i].genre_ids)
                                {
                                    Movie.Genres.Add(new Genre(genre));
                                }
                            }
                            break;
                        }
                    }
                }

                if (Movie.TMDB_ID > 0)
                {
                    TVShowDetails = await _TMDBapi.FetchTVShowDetailsAsync(Movie.TMDB_ID);

                    if (TVShowDetails != null)
                    {
                        Movie.IMDB_ID = "";
                        Movie.Year = Convert.ToDateTime(TVShowDetails.first_air_date).Year;

                        if (TVShowDetails.seasons.Count > 0 && Movie.Season != 0)
                        {
                            for (var i = 0; i < TVShowDetails.seasons.Count - 1; i++)
                            {
                                if (TVShowDetails.seasons[i].season_number == Movie.Season)
                                {
                                    //overwrite the year if was have a value from the seasons collection
                                    Movie.Year = Convert.ToDateTime(TVShowDetails.seasons[i].air_date).Year;
                                    break;
                                }
                            }
                        }

                        if (TVShowDetails.content_ratings != null)
                        {
                            if (TVShowDetails.content_ratings.results != null)
                            {
                                foreach (var rating in TVShowDetails.content_ratings.results)
                                {
                                    if (rating.iso_3166_1 == "US")
                                    {
                                        Movie.Rating = Movie.RatingFromString(rating.rating);
                                        break;
                                        
                                    }
                                }
                            }
                        }
                    }

                }
            }
            return Movie;

        }
    }
}
