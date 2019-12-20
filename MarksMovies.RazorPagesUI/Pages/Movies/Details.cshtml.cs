using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MarksMovies.Models;
using MarksMovies.TMDB;
using System.Linq;
using MarksMovies.WebServices;

namespace MarksMovies.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly WebDetailsService _service;


        public DetailsModel(WebDetailsService Service)
        {
            _service = Service;
            Movie = new Movie();
            MovieDetails = new MovieDetails();
            TVDetails = new TVShowDetails();
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

            Movie = await _service.GetAsync(id);

            if (Movie == null || Movie?.TMDB_ID <= 0)
                return NotFound();

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

                if (TVDetails == null)
                    return Page();
                
                Overview = TVDetails.overview;
                PosterURL = "https://image.tmdb.org/t/p/w185" + TVDetails.poster_path;

                foreach (var season in TVDetails.seasons)
                {
                    if (season.season_number == Movie.Season)
                    {
                        if(!string.IsNullOrEmpty(season.poster_path))
                            PosterURL = "https://image.tmdb.org/t/p/w185" + season.poster_path;
                        if(!string.IsNullOrEmpty(season.overview))
                            Overview = season.overview;
                        break;
                    }
                }

                Tagline = string.Empty;

                SeasonCount = TVDetails.number_of_seasons;

                if (TVDetails.episode_run_time?.Count() > 0)
                    Runtime = TVDetails.episode_run_time.ToList().FirstOrDefault();
            }
            
            return Page();
        }
    }
}
