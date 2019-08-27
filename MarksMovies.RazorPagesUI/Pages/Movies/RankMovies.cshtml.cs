using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MarksMovies.Models;
using MarksMovies.Services;

namespace MarksMovies.Pages.Movies
{
    public class RankMoviesModel : PageModel
    {
        private readonly RankMoviesService _service;

        public RankMoviesModel(RankMoviesService service)
        {
            _service = service;
        }


        public IList<Movie> Movies { get; set; }

        public void OnGet()
        {
            Movies = _service.OnGet();
        }

        public async Task<IActionResult> OnPostUpdateRanksAsync(string itemIds)
        {

            _ = await _service.UpdateRanksAsync(itemIds);

            return RedirectToPage();
        }
    }
}