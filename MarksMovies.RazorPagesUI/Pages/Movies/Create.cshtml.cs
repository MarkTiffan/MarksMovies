using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarksMovies.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MarksMovies.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MarksMovies.TMDB;

namespace MarksMovies.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private CreateService _service { get; set; }

        public CreateModel(CreateService service)
        {
            _service = service;
            Movie = new Movie();
            SearchMovies = new SearchMovies();
            MovieOrTVShowSelection = MovieOrTVShow.Movie;
            SearchTV = new SearchTV();
        }



        [BindProperty]
        public virtual Movie Movie { get; set; }

        [BindProperty]
        public virtual List<GenreType> SelectedGenres { get; set; }

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

            var MovieID = await _service.CreateAsync(Movie, SelectedGenres);


            return Redirect(Url.Content("~/Movies/Index") + "#" + MovieID);
        }




        public virtual async Task<IActionResult> OnPostFetchAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Movie.Title != "")
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
                    Movie = await _service.ImportMovieAsync(TMDB_ID, Movie.Title);
                else if(MovieOrTVShowSelection == MovieOrTVShow.TV)
                    Movie = await _service.ImportTVShowAsync(TMDB_ID, Movie.Title);

                if (Movie != null)
                {
                    if (Movie.Genres != null)
                    {
                        if (Movie.Genres.Count > 0)
                        {
                            foreach (var g in Movie.Genres)
                            {
                                SelectedGenres.Add(g.genre);
                            }
                        }
                    }
                }
            }

            ModelState.Clear();

            return Page();

        }

    }
}