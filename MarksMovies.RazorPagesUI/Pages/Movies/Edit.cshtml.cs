using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MarksMovies.Models;
using System.ComponentModel.DataAnnotations;
using MarksMovies.TMDB;
using MarksMovies.WebServices;

namespace MarksMovies.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly WebEditService _service;

        public EditModel(WebEditService Service)
        {
            _service = Service;
            SearchMovies = new SearchMovies();
            SearchTV = new SearchTV();
            Movie = new Movie();
            SelectedGenres = new List<GenreType>();
        }

        public SearchMovies SearchMovies { get; set; }

        public SearchTV SearchTV { get; set; }

        [BindProperty]
        public Movie Movie { get; set; }

        [BindProperty]
        public IList<GenreType> SelectedGenres { get; set; }

        [Display(Name = "Movie or Television?")]
        [BindProperty]
        public MovieOrTVShow MovieOrTVShowSelection { get; set; }


        

        public async Task<IActionResult> OnGetAsync(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }

            Movie = await _service.GetAsync(ID);

            if (Movie == null)
                return NotFound();

            MovieOrTVShowSelection = Movie.MovieOrTVShow;

            foreach (var mg in Movie.Genres)
            {
                SelectedGenres.Add(mg.genre);
            }
            
            return Page();
        }



        public async Task<IActionResult> OnPostEditAsync(int? ID)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Movie.MovieOrTVShow = MovieOrTVShowSelection;
            await _service.SaveMovieAsync(Movie, SelectedGenres);

            return Redirect(Url.Content("~/Movies/Index") + "#" + ID);
        }


        public virtual async Task<IActionResult> OnPostFetchAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Movie.Title != string.Empty)
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
                    Movie = await _service.ImportMovieAsync(TMDB_ID);
                else if(MovieOrTVShowSelection == MovieOrTVShow.TV)
                    Movie = await _service.ImportTVShowAsync(TMDB_ID);

                foreach (var g in Movie.Genres)
                {
                    SelectedGenres.Add(g.genre);
                }
            }

            ModelState.Clear();

            return Page();

        }
    }
}
