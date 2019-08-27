using MarksMovies.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MarksMovies.Pages.Movies
{
    public class ExportModel : PageModel
    {

        private readonly ExportService _service;

        public ExportModel(ExportService service)
        {
            _service = service;
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

        public FileResult OnPostDownload()
        {
            string filename;
            
            switch (FileTypeSelection){
                case ExportFileType.Json:
                    {
                        filename = "Movies.json";
                        if (!string.IsNullOrEmpty(FileName))
                            filename = FileName;

                        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(_service.GetMoviesJson());
                        return File(bytes, "application/json", filename);
                    }
                case ExportFileType.Excel:
                    {
                        filename = "Movies.xlsx";
                        if (!string.IsNullOrEmpty(FileName))
                            filename = FileName;

                        return File(_service.GetExcelFile(), 
                                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                    filename);
                    }
                default:
                    return null;
            }
        }
    }
}