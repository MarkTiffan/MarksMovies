using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MarksMovies.Models;
using MarksMovies.WebServices;

namespace MarksMovies.Pages.Movies
{
    public class RankMoviesModel : PageModel
    {
        private readonly WebRankMovieService _service;

        public RankMoviesModel(WebRankMovieService Service)
        {
            _service = Service;
        }


        public IList<Movie> Movies { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Movies = await _service.GetRankedMoviesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateRanksAsync(string ItemIds)
        {

            _ = await _service.UpdateRanksAsync(ItemIds);

            return RedirectToPage();
        }
    }
}