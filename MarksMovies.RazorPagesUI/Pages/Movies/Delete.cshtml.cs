using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MarksMovies.Models;
using MarksMovies.WebServices;

namespace MarksMovies.Pages.Movies
{
    public class DeleteModel : PageModel
    {
        private readonly WebDeleteService _service;

        public DeleteModel(WebDeleteService Service)
        {
            _service = Service;
            Movie = new Movie();
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }

            Movie = await _service.GetAsync(ID);

            if (Movie == null)
                return NotFound();
            else
                return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }

            if (await _service.DeleteMovieAsync(ID) == WebDeleteService.DELETE_FAIL)
                return NotFound();
            else
                return RedirectToPage("./Index");
        }
    }
}
