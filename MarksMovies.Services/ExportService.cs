using MarksMovies.Models;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using OfficeOpenXml;
using System.Data;
using OfficeOpenXml.Style;
using MarksMovies.DataAccess;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MarksMovies.Services
{
    public class ExportService
    {
        private readonly IMovieDBAccess _dbAccess;

        public ExportService(IMovieDBAccess DbAccess)
        {
            _dbAccess = DbAccess;
        }


        public async Task<IList<Movie>> GetMoviesAsync(string Title = "", GenreType Genre = 0)
        {
            return await _dbAccess.GetMovieListAsync(Title, Genre);
        }

        public async Task<string> GetMoviesJsonAsync()
        {
            IList<Movie> movies = await _dbAccess.GetMovieListAsync();
            if (movies == null)
                return null;

            return JArray.Parse(JsonConvert.SerializeObject(movies)).ToString();
        }


        //public async Task<Movie> GetMovieAsync(int ID)
        //{
        //    return await _dbAccess.GetMovieAsync(ID);
        //}

        private async Task<DataTable> GetMoviesDataTableAsync()
        {
            IList<Movie> movies = await _dbAccess.GetMovieListAsync();
            if (movies == null)
                return null;

            using (DataTable dt = new DataTable())
            {
                dt.Columns.Add("Title");
                dt.Columns.Add("Year");
                dt.Columns.Add("Rating");
                dt.Columns.Add("Genres");
                dt.Columns.Add("TMDB ID");
                dt.Columns.Add("IMDB ID");
                dt.Columns.Add("Media Type");
                dt.Columns.Add("Movie Or TV Show?");
                dt.Columns.Add("Season");
                dt.Columns.Add("Rank");
                dt.Columns.Add("Comments");

                foreach (var movie in movies)
                {
                    dt.Rows.Add(
                            movie.Title,
                            movie.Year,
                            movie.Rating,
                            movie.GetGenresAsString(),
                            movie.TMDB_ID,
                            movie.IMDB_ID,
                            movie.MediaType,
                            movie.MovieOrTVShow,
                            movie.Season,
                            movie.Rank,
                            movie.Comments
                        );
                }

                return dt;
            }
        }

        
        public async Task<byte[]> GetExcelFileAsync()
        {
            using (var package = new ExcelPackage())
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Movies");

                using (DataTable dt = await GetMoviesDataTableAsync())
                {
                    // add rows
                    if (dt?.Rows.Count <= 0)
                        return null;

                    // add headers
                    int col = 0;
                    foreach (DataColumn column in dt.Columns)
                    {
                        sheet.Cells[1, ++col].Value = column.ColumnName;
                        sheet.Cells[1, col].Style.Font.Bold = true;
                        sheet.Cells[1, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        sheet.Cells[1, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }



                    int row = 1;
                    for (int eachRow = 0; eachRow < dt.Rows.Count;)
                    {

                        for (int eachColumn = 1; eachColumn <= col; eachColumn++)
                        {
                            var eachRowObject = sheet.Cells[row + 1, eachColumn];
                            eachRowObject.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            eachRowObject.Value = dt.Rows[eachRow][(eachColumn - 1)].ToString();

                            eachRowObject.Style.Border.BorderAround(ExcelBorderStyle.Thin);

                            // add alternate row cell shading
                            if (eachRow % 2 == 0)
                                eachRowObject.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#e0e0e0"));
                            else
                                eachRowObject.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#ffffff"));
                        }
                        eachRow++;
                        row++;

                    }

                    sheet.Cells.AutoFitColumns();

                    return package.GetAsByteArray();
                }
            }
        }
    }
}
