﻿using MarksMovies.DataAccess;
using MarksMovies.Models;
using MarksMovies.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarksMoviesTests
{
    [TestClass]
    public class RankMoviesTest : DataAccessTestBase
    {

        [TestMethod]
        public void OnGet_VerifyMovieListOnSuccess()
        {
            IList<Movie> Movies;
            Context.Movie.Add(CommonTestFunctions.GetSampleMovie(true));
            Context.SaveChanges();

            using (var newcontext = new MarksMoviesContext(Options))
            {
                MovieDBAccess db = new MovieDBAccess(newcontext);
                var service = new RankMoviesService(db);

                Movies = service.GetRankedMovies();

                Assert.IsNotNull(Movies);
                Assert.IsTrue(Movies.Count == 1);
            }
        }


        [TestMethod]
        public async Task UpdateRanksAsync_VerifyRanksCanBeUpdated()
        {
            int result;
            var itemIds = "2,1";
            IList<Movie> Movies;
            Context.Movie.Add(CommonTestFunctions.GetSampleMovie(true,1));
            Context.Movie.Add(CommonTestFunctions.GetSampleTVShow(true,2));
            Context.SaveChanges();

            using (var newcontext = new MarksMoviesContext(Options))
            {
                MovieDBAccess db = new MovieDBAccess(newcontext);
                var service = new RankMoviesService(db);

                result = await service.UpdateRanksAsync(itemIds);
            }
            using (var newestcontext = new MarksMoviesContext(Options))
            {
                MovieDBAccess newdb = new MovieDBAccess(newestcontext);
                var service = new RankMoviesService(newdb);

                Movies = service.GetRankedMovies();

                Assert.IsNotNull(Movies);
                Assert.IsTrue(Movies.Count == 2);
                var FirstID = Movies[0].ID;
                var SecondID = Movies[1].ID;

                Assert.AreEqual(2, FirstID);
                Assert.AreEqual(1, SecondID);
            }
        }
    }
}
