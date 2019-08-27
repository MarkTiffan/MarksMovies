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

namespace MarksMoviesTests
{
    [TestClass]
    public class EditServiceTest : DataAccessTestBase
    {
        [TestMethod]
        public async Task GetAsync_VerifyNullIdReturnsNoMovie()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            int? id = null;
            Movie movie;

            var service = new EditService(dbAccess, mockTMDBapi.Object);

            movie = await service.GetAsync(id);

            using (var newcontext = new MarksMoviesContext(options))
            {
                Assert.IsNull(movie);
                Assert.AreEqual(0, await newcontext.Movie.CountAsync<Movie>());
            }
        }


        [TestMethod]
        public async Task GetAsync_VerifyValidIdReturnsMovie()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            int? id = 1;
            Movie movie;
            Movie newMovie = CommonTestFunctions.GetSampleMovie(true);

            context.Movie.Add(newMovie);
            context.SaveChanges();

            using (var newcontext = new MarksMoviesContext(options))
            {
                MovieDBAccess db = new MovieDBAccess(newcontext);
                var service = new EditService(db, mockTMDBapi.Object);

                movie = await service.GetAsync(id);

                Assert.IsNotNull(movie);
                Assert.AreEqual(1, await newcontext.Movie.CountAsync<Movie>());
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
        public async Task SaveChangesAsync_VerifyNullMovieReturnsZero()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            Movie movie = null;
            List<GenreType> SelectGenres = new List<GenreType>();

            var service = new EditService(dbAccess, mockTMDBapi.Object);

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

            context.Movie.Add(newMovie);
            context.SaveChanges();

            using (var newcontext = new MarksMoviesContext(options))
            {
                MovieDBAccess db = new MovieDBAccess(newcontext);
                var service = new EditService(db, mockTMDBapi.Object);

                var result = await service.SaveMovieAsync(newMovie, SelectGenres);
                Assert.IsTrue(result != 0);
            }
            using (var newcontext = new MarksMoviesContext(options))
            {
                var movie = await newcontext.Movie.Include(g => g.Genres).SingleAsync();
                Assert.IsNotNull(movie);
                Assert.IsNotNull(movie.Genres);
                Assert.AreEqual(1, movie.Genres.Count);
            }
        }


        [TestMethod]
        public async Task GetAsync_VerifyValidIdReturnsTVShow()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            int? id = 1;
            Movie movie;
            Movie newMovie = CommonTestFunctions.GetSampleTVShow(true);

            context.Movie.Add(newMovie);
            context.SaveChanges();

            using (var newcontext = new MarksMoviesContext(options))
            {
                MovieDBAccess db = new MovieDBAccess(newcontext);
                var service = new EditService(db, mockTMDBapi.Object);

                movie = await service.GetAsync(id);

                Assert.IsNotNull(movie);
                Assert.AreEqual(1, await newcontext.Movie.CountAsync<Movie>());
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
        public async Task SaveChangesAsync_VerifyValidTVShowIsUpdated()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            Movie newMovie = CommonTestFunctions.GetSampleTVShow(true);
            List<GenreType> SelectGenres = new List<GenreType>();
            SelectGenres.Add(GenreType.Romance);

            context.Movie.Add(newMovie);
            context.SaveChanges();

            using (var newcontext = new MarksMoviesContext(options))
            {
                MovieDBAccess db = new MovieDBAccess(newcontext);
                var service = new EditService(db, mockTMDBapi.Object);

                var result = await service.SaveMovieAsync(newMovie, SelectGenres);
                Assert.IsTrue(result != 0);
            }
            using (var newcontext = new MarksMoviesContext(options))
            {
                var movie = await newcontext.Movie.Include(g => g.Genres).SingleAsync();
                Assert.IsNotNull(movie);
                Assert.IsNotNull(movie.Genres);
                Assert.AreEqual(1, movie.Genres.Count);
            }
        }
    }
}
