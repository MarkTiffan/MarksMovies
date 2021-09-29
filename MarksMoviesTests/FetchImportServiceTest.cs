using MarksMovies;
using MarksMovies.Services;
using MarksMovies.TMDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarksMoviesTests
{
    [TestClass]
    public class FetchImportServiceTest
    {
        [TestMethod]
        public async Task FetchMovieAsync_VerifyEmptyTitleReturnsNull()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var fis = new FetchImportService(tmdbapi);
            var title = "";

            var searchmovies = await fis.FetchMovieAsync(title);

            Assert.IsTrue(searchmovies == null);
        }


        [TestMethod]
        public async Task FetchMovieAsync_VerifyValidTitleReturnsObject()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var fis = new FetchImportService(tmdbapi);
            var title = "Superman";

            var searchmovies = await fis.FetchMovieAsync(title);

            Assert.IsTrue(searchmovies != null);
            Assert.IsTrue(searchmovies.total_results > 0);
            
        }

        [TestMethod]
        public async Task ImportMovieAsync_VerifyNoTMDBIDReturnsEmptyMovieObject()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var fis = new FetchImportService(tmdbapi);
            var tmdb_id = 0;

            var Movie = await fis.ImportMovieAsync(tmdb_id);

            Assert.IsNotNull(Movie);
            Assert.IsTrue(string.IsNullOrEmpty(Movie.Title));
            Assert.IsTrue(Movie.TMDB_ID == 0);
        }

        [TestMethod]
        public async Task ImportMovieAsync_VerifyNoTitleReturnsEmptyMovieObject()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var fis = new FetchImportService(tmdbapi);
            var tmdb_id = 299534;

            var Movie = await fis.ImportMovieAsync(tmdb_id);

            Assert.IsNotNull(Movie);
            Assert.IsTrue(string.IsNullOrEmpty(Movie.Title));
            Assert.IsTrue(Movie.TMDB_ID == 0);
        }

        [TestMethod]
        public async Task ImportMovieAsync_VerifySuccessfulImport()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var fis = new FetchImportService(tmdbapi);
            var tmdb_id = 299534;
            var expectedGenreCount = 3;

            var Movie = await fis.ImportMovieAsync(tmdb_id);

            Assert.IsNotNull(Movie);
            Assert.IsTrue(!string.IsNullOrEmpty(Movie.Title));
            Assert.IsTrue(Movie.TMDB_ID == tmdb_id);
            Assert.IsNotNull(Movie.Genres);
            Assert.AreEqual(Movie.Genres.Count, expectedGenreCount);
        }




        [TestMethod]
        public async Task FetchTVShowsAsync_VerifyEmptyTitleReturnsNull()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var fis = new FetchImportService(tmdbapi);
            var title = "";

            var searchmovies = await fis.FetchTVShowsAsync(title);

            Assert.IsTrue(searchmovies == null);
        }


        [TestMethod]
        public async Task FetchTVShowsAsync_VerifyValidTitleReturnsObject()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var fis = new FetchImportService(tmdbapi);
            var title = "House";

            var searchmovies = await fis.FetchTVShowsAsync(title);

            Assert.IsTrue(searchmovies != null);
            Assert.IsTrue(searchmovies.total_results > 0);

        }

        [TestMethod]
        public async Task ImportTVShowAsync_VerifyNoTMDBIDReturnsEmptyMovieObject()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var fis = new FetchImportService(tmdbapi);
            var tmdb_id = 0;

            var Movie = await fis.ImportTVShowAsync(tmdb_id);

            Assert.IsNotNull(Movie);
            Assert.IsTrue(string.IsNullOrEmpty(Movie.Title));
            Assert.IsTrue(Movie.TMDB_ID == 0);
        }

        [TestMethod]
        public async Task ImportTVShowAsync_VerifyNoTitleReturnsEmptyMovieObject()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var fis = new FetchImportService(tmdbapi);
            var tmdb_id = 1408;

            var Movie = await fis.ImportTVShowAsync(tmdb_id);

            Assert.IsNotNull(Movie);
            Assert.IsTrue(string.IsNullOrEmpty(Movie.Title));
            Assert.IsTrue(Movie.TMDB_ID == 0);
        }

        [TestMethod]
        public async Task ImportTVShowAsync_VerifySuccessfulImport()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var fis = new FetchImportService(tmdbapi);
            var tmdb_id = 1408;
            var expectedGenreCount = 3;

            var Movie = await fis.ImportTVShowAsync(tmdb_id);

            Assert.IsNotNull(Movie);
            Assert.IsTrue(!string.IsNullOrEmpty(Movie.Title));
            Assert.IsTrue(Movie.TMDB_ID == tmdb_id);
            Assert.IsNotNull(Movie.Genres);
            Assert.AreEqual(Movie.Genres.Count, expectedGenreCount);
        }
    }
    
}
