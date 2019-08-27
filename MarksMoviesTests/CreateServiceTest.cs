using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarksMovies.Models;
using System.Collections.Generic;
using Moq;
using System.Threading.Tasks;
using MarksMovies.Services;
using MarksMovies;
using Microsoft.EntityFrameworkCore;

namespace MarksMoviesTests
{
    [TestClass]
    public class CreateServiceTest : DataAccessTestBase
    {
        [TestMethod]
        public async Task CreateAsync_VerifyNullMovieReturnsZero()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            Movie Movie = null;
            List<GenreType> SelectedGenres = null;
            var expectedResult = 0;
            int result;

            var service = new CreateService(dbAccess, mockTMDBapi.Object);

            var localresult = await service.CreateAsync(Movie, SelectedGenres);
            result = localresult; 

            using (var newcontext = new MarksMoviesContext(options))
            {
                Assert.AreEqual(0, await newcontext.Movie.CountAsync<Movie>());
                Assert.AreEqual(result, expectedResult);
            }
        }

        [TestMethod]
        public async Task CreateAsync_VerifyNewMovieIdOnSuccess()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            var title = "Avenger's Endgame";
            SearchMovies searchmovies = new SearchMovies();
            Movie Movie = CommonTestFunctions.GetSampleMovie();
            List<GenreType> SelectedGenres = new List<GenreType>()
            {
                GenreType.Action,
                GenreType.SciFi,
                GenreType.Fantasy
            };
            var expectedResult = 1;
            int result;

            mockTMDBapi.Setup(y => y.FetchMovieAsync(title)).ReturnsAsync(searchmovies);

            var service = new CreateService(dbAccess, mockTMDBapi.Object);

            result = await service.CreateAsync(Movie, SelectedGenres);
                
            using (var newcontext = new MarksMoviesContext(options))
            {
                
                Assert.AreEqual(result, expectedResult);

                Movie movie = newcontext.Movie.Include(m => m.Genres).SingleAsync<Movie>().Result;
                Assert.IsNotNull(movie);
                Assert.AreEqual(1, movie.ID);
                Assert.AreEqual("Avenger's Endgame", movie.Title);
                Assert.AreEqual(2019, movie.Year);
                Assert.AreEqual(Rating.PG13, movie.Rating);
                Assert.AreEqual(DiscType.UHD_Bluray, movie.MediaType);
                Assert.AreEqual(299534, movie.TMDB_ID);
                Assert.IsTrue(movie.Genres.Count == 3);
            }
        }

        [TestMethod]
        public async Task CreateAsync_VerifyNewMovieIdEvenWithNoGenres()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            var title = "Avenger's Endgame";
            SearchMovies searchmovies = new SearchMovies();
            Movie Movie = CommonTestFunctions.GetSampleMovie();
            List<GenreType> SelectedGenres = null;
            var expectedResult = 1;
            int result;

            mockTMDBapi.Setup(y => y.FetchMovieAsync(title)).ReturnsAsync(searchmovies);

            var service = new CreateService(dbAccess, mockTMDBapi.Object);

            result = await service.CreateAsync(Movie, SelectedGenres);

            using (var newcontext = new MarksMoviesContext(options))
            {

                Assert.AreEqual(result, expectedResult);

                Movie movie = newcontext.Movie.Include(m => m.Genres).SingleAsync<Movie>().Result;
                Assert.IsNotNull(movie);
                Assert.AreEqual(1, movie.ID);
                Assert.AreEqual("Avenger's Endgame", movie.Title);
                Assert.AreEqual(2019, movie.Year);
                Assert.AreEqual(Rating.PG13, movie.Rating);
                Assert.AreEqual(DiscType.UHD_Bluray, movie.MediaType);
                Assert.AreEqual(299534, movie.TMDB_ID);
                Assert.IsNotNull(movie.Genres);
                Assert.AreEqual(0, movie.Genres.Count);
            }
        }



        [TestMethod]
        public async Task CreateAsync_VerifyNewTVShowIdOnSuccess()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            var title = "House";
            SearchTV searchTV = new SearchTV();
            Movie Movie = CommonTestFunctions.GetSampleTVShow();
            List<GenreType> SelectedGenres = new List<GenreType>()
            {
                GenreType.Drama,
                GenreType.Comedy,
                GenreType.Mystery
            };
            var expectedResult = 1;
            int result;

            mockTMDBapi.Setup(y => y.FetchTVShowsAsync(title)).ReturnsAsync(searchTV);

            var service = new CreateService(dbAccess, mockTMDBapi.Object);

            result = await service.CreateAsync(Movie, SelectedGenres);

            using (var newcontext = new MarksMoviesContext(options))
            {

                Assert.AreEqual(result, expectedResult);

                Movie movie = newcontext.Movie.Include(m => m.Genres).SingleAsync<Movie>().Result;
                Assert.IsNotNull(movie);
                Assert.AreEqual(1, movie.ID);
                Assert.AreEqual("House", movie.Title);
                Assert.AreEqual(2004, movie.Year);
                Assert.AreEqual(Rating.TV_14, movie.Rating);
                Assert.AreEqual(DiscType.DVD, movie.MediaType);
                Assert.AreEqual(1408, movie.TMDB_ID);
                Assert.IsTrue(movie.Genres.Count == 3);
            }
        }



        [TestMethod]
        public async Task CreateAsync_VerifyNewTVShowIdEvenWithNoGenres()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            var title = "House";
            SearchTV searchTV = new SearchTV();
            Movie Movie = CommonTestFunctions.GetSampleTVShow();
            List<GenreType> SelectedGenres = null;
            var expectedResult = 1;
            int result;

            mockTMDBapi.Setup(y => y.FetchTVShowsAsync(title)).ReturnsAsync(searchTV);

            var service = new CreateService(dbAccess, mockTMDBapi.Object);

            result = await service.CreateAsync(Movie, SelectedGenres);

            using (var newcontext = new MarksMoviesContext(options))
            {

                Assert.AreEqual(result, expectedResult);

                Movie movie = newcontext.Movie.Include(m => m.Genres).SingleAsync<Movie>().Result;
                Assert.IsNotNull(movie);
                Assert.AreEqual(1, movie.ID);
                Assert.AreEqual("House", movie.Title);
                Assert.AreEqual(2004, movie.Year);
                Assert.AreEqual(Rating.TV_14, movie.Rating);
                Assert.AreEqual(DiscType.DVD, movie.MediaType);
                Assert.AreEqual(1408, movie.TMDB_ID);
                Assert.IsNotNull(movie.Genres);
                Assert.AreEqual(0, movie.Genres.Count);
            }
        }
    }
}
