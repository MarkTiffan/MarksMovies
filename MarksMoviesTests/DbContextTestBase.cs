using MarksMovies.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MarksMoviesTests
{
    [TestClass]
    public class DataAccessTestBase
    {
        protected MarksMoviesContext context { get; set; }
        protected DbContextOptions<MarksMoviesContext> options { get; set; }

        protected MovieDBAccess dbAccess { get; set; }

        [TestInitialize]
        public void Init()
        {
            options = new DbContextOptionsBuilder<MarksMoviesContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
            context = new MarksMoviesContext(options);

            dbAccess = new MovieDBAccess(context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
