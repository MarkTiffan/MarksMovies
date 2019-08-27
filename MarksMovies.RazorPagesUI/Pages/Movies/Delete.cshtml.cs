using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MarksMovies.Models;
using MarksMovies.Services;

namespace MarksMovies.Pages.Movies
{
    public class DeleteModel : PageModel
    {
        private readonly DeleteService _service;

        public DeleteModel(DeleteService service)
        {
            _service = service;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _service.GetMovieAsync(id);

            if (Movie == null)
                return NotFound();
            else
                return Page();

            
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (await _service.DeleteMovieAsync(id) == DeleteService.DELETE_FAIL)
                return NotFound();
            else
                return RedirectToPage("./Index");
        }
    }
}
