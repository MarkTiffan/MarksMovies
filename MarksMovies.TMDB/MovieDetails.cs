using System.Collections.Generic;
using MarksMovies.Models;
using System.ComponentModel.DataAnnotations;


namespace MarksMovies.TMDB
{
    public enum ReleaseDateType
    {
        Premiere = 1,
        [Display(Name ="Theatrical (Limited)")]
        TheatricalLimited = 2,
        Theatrical = 3,
        Digital = 4,
        Physical = 5,
        TV = 6
    }
    public class MovieDetails
    {
        // This class maps the result of a TMDB API call to /movie/details, 
        //     appending the TMDB API calls of /movie/release_dates and /movie/credits
        public MovieDetails()
        {
            belongs_to_collection = new BelongsToCollection();
            production_companies = new List<ProductionCompanies>();
            production_countries = new List<ProductionCountries>();
            spoken_languages = new List<SpokenLanguages>();
            release_dates = new ReleaseDates();
            genres = new List<MovieDetailGenre>();
            credits = new MovieCredits();
        }
        public bool adult { get; set; }

        public string backdrop_path { get; set; }

        public BelongsToCollection belongs_to_collection { get; set; }

        public int budget { get; set; }

        public IEnumerable<MovieDetailGenre> genres { get; set; }

        public string homepage { get; set; }

        public int id { get; set; }

        public string imdb_id { get; set; }

        public string original_language { get; set; }

        public string original_title { get; set; }

        public string overview { get; set; }

        public float popularity { get; set; }

        public string poster_path { get; set; }

        public IEnumerable<ProductionCompanies> production_companies { get; set; }

        public IEnumerable<ProductionCountries> production_countries { get; set; }

        public string release_date { get; set; }

        public long revenue { get; set; }

        public int runtime { get; set; }

        public IEnumerable<SpokenLanguages> spoken_languages { get; set; }

        public string status { get; set; }

        public string tagline { get; set; }

        public string title { get; set; }

        public bool video { get; set; }

        public float vote_average { get; set; }

        public int vote_count { get; set; }

        public ReleaseDates release_dates { get; set; }

        public MovieCredits credits { get; set; }
    }

    public class MovieDetailGenre
    {
        public GenreType id { get; set; }

        public string name { get; set; }
    }

    public class BelongsToCollection
    {
        public int id { get; set; }

        public string name { get; set; }

        public string poster_path { get; set; }

        public string backdrop_path { get; set; }
    }

    public class ProductionCompanies
    {
        public string name { get; set; }

        public int id { get; set; }

        public string logo_path { get; set; }

        public string origin_country { get; set; }
    }

    public class ProductionCountries
    {
        public string iso_3166_1 { get; set; }

        public string name { get; set; }
    }

    public class SpokenLanguages
    {
        public string iso_639_1 { get; set; }

        public string name { get; set; }
    }


    public class ReleaseDates
    {
        public ReleaseDates()
        {
            results = new List<ReleaseDateResults>();
        }
        public int id { get; set; }

        public IEnumerable<ReleaseDateResults> results { get; set; }
    }

    public class ReleaseDateResults
    {
        public ReleaseDateResults()
        {
            release_dates = new List<ReleaseDate>();
        }
        public string iso_3166_1 { get; set; }

        public IEnumerable<ReleaseDate> release_dates { get; set; }
    }

    public class ReleaseDate
    {
        public string certification { get; set; }

        public string iso_639_1 { get; set; }

        public string release_date { get; set; }

        public ReleaseDateType type { get; set; }

        public string note { get; set; }
    }



    public class MovieCredits
    {
        public MovieCredits()
        {
            cast = new List<CastMember>();
            crew = new List<CrewMember>();
        }

        public int id { get; set; }

        public IEnumerable<CastMember> cast { get; set; }

        public IEnumerable<CrewMember> crew { get; set; }
    }


    public class CastMember
    {
        public int cast_id { get; set; }

        public string character { get; set; }

        public string credit_id { get; set; }

        public int gender { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public int order { get; set; }

        public string profile_path { get; set; }
    }


    public class CrewMember
    {
        public string credit_id { get; set; }

        public string department { get; set; }

        public int gender { get; set; }

        public int id { get; set; }

        public string job { get; set; }

        public string name { get; set; }

        public string profile_path { get; set; }
    }

}
