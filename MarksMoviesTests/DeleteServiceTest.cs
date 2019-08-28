using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarksMovies.Models;
using MarksMovies.DataAccess;
using System.Threading.Tasks;
using MarksMovies.Services;
using Microsoft.EntityFrameworkCore;

namespace MarksMoviesTests
{
    [TestClass]
    public class DeleteServiceTest : DataAccessTestBase
    {
        [TestMethod]
        public async Task GetMovieAsync_VerifyNullIdReturnsNoMovie()
        {
            int? id = null;
            Movie movie;

            var service = new DeleteService(dbAccess);

            movie = await service.GetMovieAsync(id);
            Assert.IsNull(movie);
            Assert.AreEqual(0, await context.Movie.CountAsync<Movie>());
        }


        [TestMethod]
        public async Task GetMovieAsync_VerifyValidIdReturnsMovie()
        {
            int? id = 1;
            Movie movie;
            Movie newMovie = CommonTestFunctions.GetSampleMovie(true);

            context.Movie.Add(newMovie);
            context.SaveChanges();

            using (var newcontext = new MarksMoviesContext(options))
            {
                MovieDBAccess db = new MovieDBAccess(newcontext);
                var service = new DeleteService(db);

                movie = await service.GetMovieAsync(id);

                Assert.IsNotNull(movie);
                Assert.AreEqual(1, await context.Movie.CountAsync<Movie>());
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
        public async Task DeleteMovieAsync_VerifyInvalidIdReturnsDELETE_FAIL()
        {
            int? id = 0;
            Movie newMovie = CommonTestFunctions.GetSampleMovie(true);

            context.Movie.Add(newMovie);
            context.SaveChanges();

            using (var newcontext = new MarksMoviesContext(options))
            {
                MovieDBAccess db = new MovieDBAccess(newcontext);
                var service = new DeleteService(db);

                var result = await service.DeleteMovieAsync(id);
                Assert.AreEqual(DeleteService.DELETE_FAIL, result);

                Assert.AreEqual(1, await context.Movie.CountAsync<Movie>());
            }
        }


        [TestMethod]
        public async Task DeleteMovieAsync_VerifyValidIdReturnsDELETE_OK()
        {
            int? id = 1;
            Movie newMovie = CommonTestFunctions.GetSampleMovie(true);

            context.Movie.Add(newMovie);
            context.SaveChanges();

            using (var newcontext = new MarksMoviesContext(options))
            {
                MovieDBAccess db = new MovieDBAccess(newcontext);
                var service = new DeleteService(db);

                var result = await service.DeleteMovieAsync(id);
                Assert.AreEqual(DeleteService.DELETE_OK, result);
                Assert.AreEqual(0, await context.Movie.CountAsync<Movie>());
            }
        }
    }
}
