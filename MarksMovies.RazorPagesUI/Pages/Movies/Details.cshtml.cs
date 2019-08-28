using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MarksMovies.Models;
using MarksMovies.Services;
using MarksMovies.TMDB;

namespace MarksMovies.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly DetailsService _service;


        public DetailsModel(DetailsService service)
        {
            _service = service;
        }

        public Movie Movie { get; set; }

        public string PosterURL { get; set; }

        public string Overview { get; set; }

        public string Tagline { get; set; }

        public int Runtime { get; set; }

        public int SeasonCount { get; set; }

        public MovieDetails MovieDetails {get;set;}

        public TVShowDetails TVDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _service.GetMovieAsync(id);

            if (Movie == null)
            {
                return NotFound();
            }
            else
            {
                if (Movie.TMDB_ID > 0)
                {
                    if (Movie.MovieOrTVShow == MovieOrTVShow.Movie)
                    {
                        MovieDetails = await _service.GetMovieDetailsAsync(Movie.TMDB_ID);

                        if (MovieDetails != null)
                        {
                            PosterURL = "https://image.tmdb.org/t/p/w185" + MovieDetails.poster_path;

                            Overview = MovieDetails.overview;

                            Tagline = MovieDetails.tagline;

                            Runtime = MovieDetails.runtime;
                        }
                    } else if (Movie.MovieOrTVShow == MovieOrTVShow.TV)
                    {
                        TVDetails = await _service.GetTVSHowDetailsAsync(Movie.TMDB_ID);

                        if(TVDetails != null)
                        {
                            Overview = TVDetails.overview;
                            PosterURL = "https://image.tmdb.org/t/p/w185" + TVDetails.poster_path;
                            if (Movie.Season != 0)
                            {
                                if (TVDetails.seasons.Count > 0)
                                { 
                                    for (var i = 0; i < TVDetails.number_of_seasons - 1; i++)
                                    {
                                        if (TVDetails.seasons[i].season_number == Movie.Season)
                                        {
                                            if(!string.IsNullOrEmpty(TVDetails.seasons[i].poster_path))
                                                PosterURL = "https://image.tmdb.org/t/p/w185" + TVDetails.seasons[i].poster_path;
                                            if(!string.IsNullOrEmpty(TVDetails.seasons[i].overview))
                                                Overview = TVDetails.seasons[i].overview;
                                            break;
                                        }
                                    }
                                }
                            }

                            Tagline = "";

                            SeasonCount = TVDetails.number_of_seasons;

                            if (TVDetails.episode_run_time.Count > 0)
                                Runtime = TVDetails.episode_run_time[0];
                        }
                    }
                }
            }

            return Page();
        }
    }
}
