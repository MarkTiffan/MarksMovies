using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MarksMovies.Models;
using MarksMovies.WebServices;

namespace MarksMovies.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly WebMovieIndexService _service;

        public IndexModel(WebMovieIndexService Service)
        {
            _service = Service;
        }

        public IList<Movie> Movie { get; set; }

        [BindProperty(SupportsGet =true)]
        public string SearchString { get; set; }
        

        [BindProperty(SupportsGet =true)]
        public GenreType MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            Movie = await _service.OnGetAsync(SearchString, MovieGenre);
        }



    }
}
