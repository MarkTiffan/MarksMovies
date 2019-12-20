using MarksMovies.WebServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace MarksMovies.Pages.Movies
{
    public class ExportModel : PageModel
    {

        private readonly WebExportService _service;

        public ExportModel(WebExportService Service)
        {
            _service = Service;
        }

        public enum ExportFileType
        {
            Json,
            CSV,
            Excel
        }

        [BindProperty]
        public ExportFileType FileTypeSelection { get; set; }

        [BindProperty]
        public string FileName { get; set; }

        public void OnGet()
        {

        }

        public async Task<FileResult> OnPostDownloadAsync()
        {
            string filename;
            
            switch (FileTypeSelection){
                case ExportFileType.Json:
                    {
                        filename = "Movies.json";
                        if (!string.IsNullOrEmpty(FileName))
                            filename = FileName;

                        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(await _service.GetMoviesJsonAsync());
                        return File(bytes, "application/json", filename);
                    }
                case ExportFileType.Excel:
                    {
                        filename = "Movies.xlsx";
                        if (!string.IsNullOrEmpty(FileName))
                            filename = FileName;

                        return File(await _service.GetExcelFileAsync(), 
                                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                    filename);
                    }
                default:
                    return null;
            }
        }
    }
}