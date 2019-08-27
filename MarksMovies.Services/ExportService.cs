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

        public ExportService(IMovieDBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }


        public IQueryable<Movie> GetMovies()
        {
            IQueryable<Movie> Movies = _dbAccess.GetMovieList();
            if (Movies == null)
                return null;
            else
                return Movies;
        }

        public string GetMoviesJson()
        {
            IQueryable<Movie> Movies = _dbAccess.GetMovieList();
            if (Movies == null)
                return null;
            else
                return JObject.Parse(JsonConvert.SerializeObject(Movies)).ToString(); ;
        }


        public async Task<Movie> GetMovie(int ID)
        {
            var Movie = await _dbAccess.GetMovieAsync(ID);
            if (Movie == null)
                return null;
            else
                return Movie;
        }

        public DataTable GetMoviesDataTable()
        {
            IList<Movie> Movies = _dbAccess.GetMovieList().ToList();

            using (DataTable DT = new DataTable())
            {

                if (Movies != null)
                {
                    DT.Columns.Add("Title");
                    DT.Columns.Add("Year");
                    DT.Columns.Add("Rating");
                    DT.Columns.Add("Genres");
                    DT.Columns.Add("TMDB ID");
                    DT.Columns.Add("IMDB ID");
                    DT.Columns.Add("Media Type");
                    DT.Columns.Add("Movie Or TV Show?");
                    DT.Columns.Add("Season");
                    DT.Columns.Add("Rank");
                    DT.Columns.Add("Comments");

                    for (var row = 0; row < Movies.Count; row++)
                    {
                        DT.Rows.Add(
                                Movies[row].Title,
                                Movies[row].Year,
                                Movies[row].Rating,
                                Movies[row].GetGenresAsString(),
                                Movies[row].TMDB_ID,
                                Movies[row].IMDB_ID,
                                Movies[row].MediaType,
                                Movies[row].MovieOrTVShow,
                                Movies[row].Season,
                                Movies[row].Rank,
                                Movies[row].Comments
                            );
                    }

                    return DT;
                }
                else
                    return null;
            }
        }


        public byte[] GetExcelFile()
        {
            using (var package = new ExcelPackage())
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Movies");

                using (DataTable DT = GetMoviesDataTable())
                {

                    // add headers
                    int col = 0;
                    foreach (DataColumn column in DT.Columns)
                    {
                        sheet.Cells[1, ++col].Value = column.ColumnName;
                        sheet.Cells[1, col].Style.Font.Bold = true;
                        sheet.Cells[1, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        sheet.Cells[1, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    // add rows
                    if (DT.Rows.Count <= 0)
                        return null;
                    else
                    {
                        int row = 1;
                        for (int eachRow = 0; eachRow < DT.Rows.Count;)
                        {

                            for (int eachColumn = 1; eachColumn <= col; eachColumn++)
                            {
                                var eachRowObject = sheet.Cells[row + 1, eachColumn];
                                eachRowObject.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                eachRowObject.Value = DT.Rows[eachRow][(eachColumn - 1)].ToString();

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

                    }
                    sheet.Cells.AutoFitColumns();
                    return package.GetAsByteArray();

                }
            }
        }
    }
}
