using MarksMovies.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MarksMoviesTests
{
    [TestClass]
    public class DataAccessTestBase
    {
        protected MarksMoviesContext Context { get; set; }
        protected DbContextOptions<MarksMoviesContext> Options { get; set; }

        protected MovieDBAccess DbAccess { get; set; }

        [TestInitialize]
        public void Init()
        {
            Options = new DbContextOptionsBuilder<MarksMoviesContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
            Context = new MarksMoviesContext(Options);

            DbAccess = new MovieDBAccess(Context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
