using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarksMovies.Models;
using System.Collections.Generic;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using MarksMovies.Services;
using MarksMovies;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using MarksMovies.DataAccess;
using MarksMovies.TMDB;

namespace MarksMoviesTests
{
    [TestClass]
    public class EditServiceTest : DataAccessTestBase
    {
 

        [TestMethod]
        public async Task SaveChangesAsync_VerifyNullMovieReturnsZero()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            Movie movie = null;
            List<GenreType> SelectGenres = new List<GenreType>();

            var service = new EditService(DbAccess, mockTMDBapi.Object);

            var result = await service.SaveMovieAsync(movie, SelectGenres);
            Assert.AreEqual(0, result);
        }



        [TestMethod]
        public async Task SaveChangesAsync_VerifyValidMovieIsUpdated()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            Movie newMovie = CommonTestFunctions.GetSampleMovie(true);
            List<GenreType> SelectGenres = new List<GenreType>();
            SelectGenres.Add(GenreType.Comedy);

            Context.Movie.Add(newMovie);
            Context.SaveChanges();

            using (var newcontext = new MarksMoviesContext(Options))
            {
                MovieDBAccess db = new MovieDBAccess(newcontext);
                var service = new EditService(db, mockTMDBapi.Object);

                var result = await service.SaveMovieAsync(newMovie, SelectGenres);
                Assert.IsTrue(result != 0);
            }
            using (var newcontext = new MarksMoviesContext(Options))
            {
                var movie = await newcontext.Movie.Include(g => g.Genres).SingleAsync();
                Assert.IsNotNull(movie);
                Assert.IsNotNull(movie.Genres);
                Assert.AreEqual(1, movie.Genres.Count);
            }
        }




        [TestMethod]
        public async Task SaveChangesAsync_VerifyValidTVShowIsUpdated()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            Movie newMovie = CommonTestFunctions.GetSampleTVShow(true);
            List<GenreType> SelectGenres = new List<GenreType>();
            SelectGenres.Add(GenreType.Romance);

            Context.Movie.Add(newMovie);
            Context.SaveChanges();

            using (var newcontext = new MarksMoviesContext(Options))
            {
                MovieDBAccess db = new MovieDBAccess(newcontext);
                var service = new EditService(db, mockTMDBapi.Object);

                var result = await service.SaveMovieAsync(newMovie, SelectGenres);
                Assert.IsTrue(result != 0);
            }
            using (var newcontext = new MarksMoviesContext(Options))
            {
                var movie = await newcontext.Movie.Include(g => g.Genres).SingleAsync();
                Assert.IsNotNull(movie);
                Assert.IsNotNull(movie.Genres);
                Assert.AreEqual(1, movie.Genres.Count);
            }
        }
    }
}
