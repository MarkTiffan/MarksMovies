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
        public async Task DeleteMovieAsync_VerifyInvalidIdReturnsDELETE_FAIL()
        {
            int? id = 0;
            Movie newMovie = CommonTestFunctions.GetSampleMovie(true);

            Context.Movie.Add(newMovie);
            Context.SaveChanges();

            using (var newcontext = new MarksMoviesContext(Options))
            {
                MovieDBAccess db = new MovieDBAccess(newcontext);
                var service = new DeleteService(db);

                var result = await service.DeleteMovieAsync(id);
                Assert.AreEqual(DeleteService.DELETE_FAIL, result);

                Assert.AreEqual(1, await Context.Movie.CountAsync<Movie>());
            }
        }


        [TestMethod]
        public async Task DeleteMovieAsync_VerifyValidIdReturnsDELETE_OK()
        {
            int? id = 1;
            Movie newMovie = CommonTestFunctions.GetSampleMovie(true);

            Context.Movie.Add(newMovie);
            Context.SaveChanges();

            using (var newcontext = new MarksMoviesContext(Options))
            {
                MovieDBAccess db = new MovieDBAccess(newcontext);
                var service = new DeleteService(db);

                var result = await service.DeleteMovieAsync(id);
                Assert.AreEqual(DeleteService.DELETE_SUCCESS, result);
                Assert.AreEqual(0, await Context.Movie.CountAsync<Movie>());
            }
        }
    }
}
