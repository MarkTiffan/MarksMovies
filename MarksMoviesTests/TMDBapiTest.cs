using MarksMovies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using MarksMovies.Models;

namespace MarksMoviesTests
{
    [TestClass]
    public class TMDBapiTest
    {
        [TestMethod]
        public async Task FetchMovie_VerifyEmptyTitleReturnsNull()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var title = "";

            var searchmovies = await tmdbapi.FetchMovieAsync(title);

            Assert.IsTrue(searchmovies == null);
        }

        [TestMethod]
        public async Task FetchMovie_VerifySuccessfulFetch()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var title = "Avengers Endg";
            var expectedTMDB_ID = 299534;
            var expectedResultCount = 1;

            var searchmovies = await tmdbapi.FetchMovieAsync(title);

            Assert.IsNotNull(searchmovies);
            Assert.AreEqual(searchmovies.total_results, expectedResultCount);
            Assert.AreEqual(searchmovies.results.Count, expectedResultCount);
            Assert.AreEqual(searchmovies.results[0].id, expectedTMDB_ID);
        }

        [TestMethod]
        public async Task FetchMovieDetails_VerifyZeroTMDBIDReturnsNull()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var tmdb_id = 0;

            var moviedetails = await tmdbapi.FetchMovieDetailsAsync(tmdb_id);

            Assert.IsTrue(moviedetails == null);
        }

        [TestMethod]
        public async Task FetchMovieDetails_VerifyNegativeTMDBIDReturnsNull()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var tmdb_id = -1;

            var moviedetails = await tmdbapi.FetchMovieDetailsAsync(tmdb_id);

            Assert.IsTrue(moviedetails == null);
        }

        [TestMethod]
        public async Task FetchMovieDetails_VerifySuccessfulFetchDetails()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var tmdb_id = 299534;
            var expectedGenreCount = 3;
            var TonyStarkExists = false;
            var expectedName = "Tony Stark / Iron Man";

            var moviedetails = await tmdbapi.FetchMovieDetailsAsync(tmdb_id);

            Assert.IsNotNull(moviedetails);
            Assert.AreEqual(moviedetails.genres.Count, expectedGenreCount);
            Assert.IsNotNull(moviedetails.credits);
            Assert.IsNotNull(moviedetails.credits.cast);
            Assert.IsTrue(moviedetails.credits.cast.Count > 0);
            foreach (var person in moviedetails.credits.cast)
            {
                if(person.character == expectedName)
                {
                    TonyStarkExists = true;
                    break;
                }
            }
            Assert.IsTrue(TonyStarkExists);
        }




        [TestMethod]
        public async Task FetchTVShows_VerifyEmptyTitleReturnsNull()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var title = "";

            var searchTVShows = await tmdbapi.FetchTVShowsAsync(title);

            Assert.IsTrue(searchTVShows == null);
        }

        [TestMethod]
        public async Task FetchTVShows_VerifySuccessfulFetch()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var title = "House M.D.";
            var expectedTMDB_ID = 1408;
            var expectedResultCount = 1;

            var searchTVShows = await tmdbapi.FetchTVShowsAsync(title);

            Assert.IsNotNull(searchTVShows);
            Assert.AreEqual(searchTVShows.total_results, expectedResultCount);
            Assert.AreEqual(searchTVShows.results.Count, expectedResultCount);
            Assert.AreEqual(searchTVShows.results[0].id, expectedTMDB_ID);
        }

        [TestMethod]
        public async Task FFetchTVShowDetails_VerifyZeroTMDBIDReturnsNull()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var tmdb_id = 0;

            var TVShowDetails = await tmdbapi.FetchTVShowDetailsAsync(tmdb_id);

            Assert.IsTrue(TVShowDetails == null);
        }

        [TestMethod]
        public async Task FetchTVShowDetails_VerifyNegativeTMDBIDReturnsNull()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var tmdb_id = -1;

            var TVShowDetails = await tmdbapi.FetchTVShowDetailsAsync(tmdb_id);

            Assert.IsTrue(TVShowDetails == null);
        }

        [TestMethod]
        public async Task FetchTVShowDetails_VerifySuccessfulFetchDetails()
        {
            var httpclient = new HttpClient();
            var tmdbapi = new TMDBapi(httpclient);
            var tmdb_id = 1408;
            var expectedGenreCount = 3;
            var GregoryHouseExists = false;
            var expectedName = "Gregory House";
            Rating expectedRating = Rating.TV_14;
            Rating? rating = null;

            var TVShowDetails = await tmdbapi.FetchTVShowDetailsAsync(tmdb_id);

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
            Assert.IsNotNull(TVShowDetails.content_ratings);
            Assert.IsNotNull(TVShowDetails.content_ratings.results);
            Assert.IsTrue(TVShowDetails.content_ratings.results.Count > 0);
            foreach(var country in TVShowDetails.content_ratings.results)
            {
                if(country.iso_3166_1 == "US")
                {
                    rating = Movie.RatingFromString(country.rating);
                    break;
                }
            }
            Assert.IsNotNull(rating);
            Assert.AreEqual(rating, expectedRating);
        }
    }
}
