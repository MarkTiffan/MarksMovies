using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarksMovies.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MarksMovies.TMDB;
using MarksMovies.WebServices;

namespace MarksMovies.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private WebCreateService _service { get; set; }

        public CreateModel(WebCreateService Service)
        {
            _service = Service;
            Movie = new Movie();
            SearchMovies = new SearchMovies();
            MovieOrTVShowSelection = MovieOrTVShow.Movie;
            SearchTV = new SearchTV();
        }



        [BindProperty]
        public virtual Movie Movie { get; set; }

        [BindProperty]
        public virtual IList<GenreType> SelectedGenres { get; set; }

        public virtual SearchMovies SearchMovies { get; set; }

        public virtual SearchTV SearchTV { get; set; }

        [Display(Name = "Movie or Television?")]
        [BindProperty]
        public MovieOrTVShow MovieOrTVShowSelection { get; set; }


        public IActionResult OnGet()
        {
            return Page();
        }


        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Movie.MovieOrTVShow = MovieOrTVShowSelection;
            var MovieID = await _service.CreateAsync(Movie, SelectedGenres);


            return Redirect(Url.Content("~/Movies/Index") + "#" + MovieID);
        }




        public virtual async Task<IActionResult> OnPostFetchAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Movie.Title != string.Empty)
                if(MovieOrTVShowSelection == MovieOrTVShow.Movie)
                    SearchMovies = await _service.FetchMovieAsync(Movie.Title);
                else if(MovieOrTVShowSelection == MovieOrTVShow.TV)
                    SearchTV = await _service.FetchTVShowsAsync(Movie.Title);


            return Page();
        }



        public virtual async Task<IActionResult> OnPostImportAsync(int TMDB_ID)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            if (TMDB_ID > 0)
            {

                if (SelectedGenres.Count > 0)
                {
                    SelectedGenres.Clear();
                }

                if(MovieOrTVShowSelection == MovieOrTVShow.Movie)
                    Movie = await _service.ImportMovieAsync(TMDB_ID, Movie.Title, Movie);
                else if(MovieOrTVShowSelection == MovieOrTVShow.TV)
                    Movie = await _service.ImportTVShowAsync(TMDB_ID, Movie.Title, Movie);

                if (Movie?.Genres?.Count > 0)
                {
                    foreach (var g in Movie.Genres)
                    {
                        SelectedGenres.Add(g.genre);
                    }
                }
            }

            ModelState.Clear();

            return Page();

        }

    }
}