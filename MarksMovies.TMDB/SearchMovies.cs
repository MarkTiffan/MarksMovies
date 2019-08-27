using System.Collections.Generic;
using MarksMovies.Models;

namespace MarksMovies
{
    public class SearchMovies
    {
        // This class maps to the TMDB API call for /search/movie
        public SearchMovies()
        {
            results = new List<SearchMoviesResult>();
        }
        public int page { get; set; }

        public int total_pages { get; set; }

        public int total_results { get; set; }

        public IList<SearchMoviesResult> results { get; set; }
    }

    public class SearchMoviesResult
    {
        public string poster_path { get; set; }

        public bool adult { get; set; }

        public string overview { get; set; }

        public string release_date { get; set; }

        public IList<GenreType> genre_ids { get; set; }

        public int id { get; set; }

        public string original_title { get; set; }

        public string original_language { get; set; }

        public string title { get; set; }

        public string backdrop_path { get; set; }

        public float popularity { get; set; }

        public int vote_count { get; set; }

        public bool video { get; set; }

        public float vote_average { get; set; }


    }
}
