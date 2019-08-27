using MarksMovies;
using MarksMovies.DataAccess;
using MarksMovies.Models;
using MarksMovies.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarksMoviesTests
{
    [TestClass]
    public class DetailsServiceTest : DataAccessTestBase
    {
        [TestMethod]
        public async Task GetMovieAsync_VerifyNullIdReturnsNoMovie()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            int? id = null;
            Movie movie;

            var service = new DetailsService(dbAccess, mockTMDBapi.Object);

            movie = await service.GetMovieAsync(id);
            Assert.IsNull(movie);
            Assert.AreEqual(0, await context.Movie.CountAsync<Movie>());
        }

        [TestMethod]
        public async Task GetMovieAsync_VerifyZeroIdReturnsNoMovie()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            int? id = 0;
            Movie movie;

            var service = new DetailsService(dbAccess, mockTMDBapi.Object);

            movie = await service.GetMovieAsync(id);
            Assert.IsNull(movie);
            Assert.AreEqual(0, await context.Movie.CountAsync<Movie>());
        }

        [TestMethod]
        public async Task GetMovieAsync_VerifyNegativeIdReturnsNoMovie()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            int? id = -1;
            Movie movie;

            var service = new DetailsService(dbAccess, mockTMDBapi.Object);

            movie = await service.GetMovieAsync(id);
            Assert.IsNull(movie);
            Assert.AreEqual(0, await context.Movie.CountAsync<Movie>());
        }

        [TestMethod]
        public async Task GetMovieAsync_VerifyValidIdReturnsMovie()
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
                var service = new DetailsService(db, mockTMDBapi.Object);

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
        public async Task GetMovieDetailsAsync_VerifyZeroIdReturnsNoMovie()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            int tmdb_id = 0;

            var service = new DetailsService(dbAccess, mockTMDBapi.Object);

            var moviedetails = await service.GetMovieDetailsAsync(tmdb_id);
            Assert.IsNull(moviedetails);
        }


        [TestMethod]
        public async Task GetMovieDetailsAsync_VerifySuccessReturnsMovieDetails()
        {
            var httpclient = new HttpClient();
            var TMDBapi = new TMDBapi(httpclient);
            var tmdb_id = 299534;
            var expectedGenreCount = 3;
            var TonyStarkExists = false;
            var expectedName = "Tony Stark / Iron Man";

            var service = new DetailsService(dbAccess, TMDBapi);

            var moviedetails = await service.GetMovieDetailsAsync(tmdb_id);

            Assert.IsNotNull(moviedetails);
            Assert.AreEqual(moviedetails.genres.Count, expectedGenreCount);
            Assert.IsNotNull(moviedetails.credits);
            Assert.IsNotNull(moviedetails.credits.cast);
            Assert.IsTrue(moviedetails.credits.cast.Count > 0);
            foreach (var person in moviedetails.credits.cast)
            {
                if (person.character == expectedName)
                {
                    TonyStarkExists = true;
                    break;
                }
            }
            Assert.IsTrue(TonyStarkExists);
        }


        [TestMethod]
        public async Task GetTVShowDetailsAsync_VerifyZeroIdReturnsNoMovie()
        {
            var mockTMDBapi = new Mock<ITMDBapi>();
            int tmdb_id = 0;

            var service = new DetailsService(dbAccess, mockTMDBapi.Object);

            var TVShowDetails = await service.GetTVSHowDetailsAsync(tmdb_id);
            Assert.IsNull(TVShowDetails);
        }



        [TestMethod]
        public async Task GetTVShowDetailsAsync_VerifySuccessReturnsMovieDetails()
        {
            var httpclient = new HttpClient();
            var TMDBapi = new TMDBapi(httpclient);
            var tmdb_id = 1408;
            var expectedGenreCount = 3;
            var GregoryHouseExists = false;
            var expectedName = "Gregory House";

            var service = new DetailsService(dbAccess, TMDBapi);

            var TVShowDetails = await service.GetTVSHowDetailsAsync(tmdb_id);

            Assert.IsNotNull(TVShowDetails);
            Assert.AreEqual(TVShowDetails.genres.Count, expectedGenreCount);
            Assert.IsNotNull(TVShowDetails.credits);
            Assert.IsNotNull(TVShowDetails.credits.cast);
            Assert.IsTrue(TVShowDetails.credits.cast.Count > 0);
            foreach (var person in TVShowDetails.credits.cast)
            {
                if (person.character == expectedName)
                {
                    GregoryHouseExists = true;
                    break;
                }
            }
            Assert.IsTrue(GregoryHouseExists);
        }
    }
}
