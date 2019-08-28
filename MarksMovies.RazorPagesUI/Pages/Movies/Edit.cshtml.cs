using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MarksMovies.Models;
using MarksMovies.Services;
using System.ComponentModel.DataAnnotations;
using MarksMovies.TMDB;

namespace MarksMovies.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly EditService _service;

        public SearchMovies SearchMovies { get; set; }

        public SearchTV SearchTV { get; set; }

        [BindProperty]
        public Movie Movie { get; set; }

        [BindProperty]
        public List<GenreType> SelectedGenres { get; set; }

        [Display(Name = "Movie or Television?")]
        [BindProperty]
        public MovieOrTVShow MovieOrTVShowSelection { get; set; }

        public EditModel(EditService service)
        {
            _service = service;
        }
        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            Movie = await _service.GetAsync(id);

            if (Movie == null)
            {
                return NotFound();
            }
            else
            {
                MovieOrTVShowSelection = Movie.MovieOrTVShow;

                if (Movie.Genres.Count() > 0)
                {
                    SelectedGenres = new List<GenreType>();

                    foreach (var item in Movie.Genres)
                    {
                        SelectedGenres.Add(item.genre);
                    }
                }
            }
            return Page();
        }



        public async Task<IActionResult> OnPostEditAsync(int? id)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Movie.MovieOrTVShow = MovieOrTVShowSelection;
            await _service.SaveMovieAsync(Movie, SelectedGenres);

            return Redirect(Url.Content("~/Movies/Index") + "#" + id);
        }


        public virtual async Task<IActionResult> OnPostFetchAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Movie.Title != "")
            {
                if (MovieOrTVShowSelection == MovieOrTVShow.Movie)
                    SearchMovies = await _service.FetchMovieAsync(Movie.Title);
                else if (MovieOrTVShowSelection == MovieOrTVShow.TV)
                    SearchTV = await _service.FetchTVShowsAsync(Movie.Title);
            }
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

            ModelState.Clear();

            return Page();

        }
    }
}
