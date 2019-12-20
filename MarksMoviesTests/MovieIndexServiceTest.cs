using MarksMovies.DataAccess;
using MarksMovies.Models;
using MarksMovies.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarksMoviesTests
{
    [TestClass]
    public class MovieIndexServiceTest : DataAccessTestBase
    {
        [TestMethod]
        public async Task OnGetAsync_VerifyNoQueryStringReturnsMovieList()
        {
            IList<Movie> Movies;
            Context.Movie.Add(CommonTestFunctions.GetSampleMovie(true));
            Context.SaveChanges();

            using (var context = new MarksMoviesContext(Options))
            {
                var DbAccess = new MovieDBAccess(context);
                var service = new MovieIndexService(DbAccess);

                Movies = await service.OnGetAsync("", 0);

                Assert.IsNotNull(Movies);
                Assert.IsTrue(Movies.Count() > 0);
            }
        }


        [TestMethod]
        public async Task OnGetAsync_VerifyQueryStringReturnsMovieList()
        {
            IList<Movie> Movies;
            string expectedTitle = "Avenger's Endgame";
            string SearchString = "avenger";
            Context.Movie.Add(CommonTestFunctions.GetSampleMovie(true));
            Context.Movie.Add(CommonTestFunctions.GetSampleTVShow(true, 2));
            Context.SaveChanges();

            using (var context = new MarksMoviesContext(Options))
            {
                var DbAccess = new MovieDBAccess(context);
                var service = new MovieIndexService(DbAccess);

                Movies = await service.OnGetAsync(SearchString, 0);

                Assert.IsNotNull(Movies);
                Assert.IsTrue(Movies.Count() == 1);
                Assert.AreEqual(expectedTitle, Movies[0].Title);
            }
        }


        [TestMethod]
        public async Task OnGetAsync_VerifyGenreSelectionReturnsMovieList()
        {
            IList<Movie> Movies;
            string expectedTitle = "House";
            GenreType selection = GenreType.Drama;
            Context.Movie.Add(CommonTestFunctions.GetSampleMovie(true));
            Context.Movie.Add(CommonTestFunctions.GetSampleTVShow(true, 2));
            Context.SaveChanges();

            using (var context = new MarksMoviesContext(Options))
            {
                var DbAccess = new MovieDBAccess(context);
                var service = new MovieIndexService(DbAccess);

                Movies = await service.OnGetAsync("", selection);

                Assert.IsNotNull(Movies);
                Assert.IsTrue(Movies.Count() == 1);
                Assert.AreEqual(expectedTitle, Movies[0].Title);
            }
        }
    }
}
