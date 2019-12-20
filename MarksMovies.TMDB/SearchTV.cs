using System.Collections.Generic;
using MarksMovies.Models;

namespace MarksMovies.TMDB
{
    public class SearchTV
    {
        public SearchTV()
        {
            results = new List<SearchTVResult>();
        }
        public int page { get; set; }

        public IEnumerable<SearchTVResult> results { get; set; }

        public int total_results { get; set; }

        public int total_pages { get; set; }
    }


    public class SearchTVResult
    {
        public SearchTVResult()
        {
            origin_country = new List<string>();
            genre_ids = new List<GenreType>();
        }
        public string poster_path { get; set; }

        public float popularity { get; set; }

        public int id { get; set; }

        public string backdrop_path { get; set; }

        public float vote_average { get; set; }

        public string overview { get; set; }

        public string first_air_date { get; set; }

        public IEnumerable<string> origin_country { get; set; }

        public IEnumerable<GenreType> genre_ids { get; set; }

        public string original_language { get; set; }

        public int vote_count { get; set; }

        public string name { get; set; }

        public string original_name { get; set; }
    }

}
