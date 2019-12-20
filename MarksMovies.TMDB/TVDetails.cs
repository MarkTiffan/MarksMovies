using System.Collections.Generic;
using MarksMovies.Models;

namespace MarksMovies.TMDB
{
    public class TVShowDetails
    {
        public TVShowDetails()
        {
            created_by = new List<TVShowCreatedBy>();
            episode_run_time = new List<int>();
            genres = new List<TVShowGenre>();
            languages = new List<string>();
            last_episode_to_air = new TVShowEpisodeToAir();
            next_episode_to_air = new TVShowEpisodeToAir();
            networks = new List<TVShowNetworks>();
            origin_country = new List<string>();
            production_companies = new List<ProductionCompanies>();
            seasons = new List<TVShowSeason>();
            credits = new TVCredits();
            content_ratings = new TVRatings();
        }

        public string backdrop_path { get; set; }

        public IEnumerable<TVShowCreatedBy> created_by { get; set; }

        public IEnumerable<int> episode_run_time { get; set; }

        public string first_air_date { get; set; }

        public IEnumerable<TVShowGenre> genres { get; set; }

        public string homepage { get; set; }

        public int id { get; set; }

        public bool in_production { get; set; }

        public IEnumerable<string> languages { get; set; }

        public string last_air_date { get; set; }

        public TVShowEpisodeToAir last_episode_to_air { get; set; }

        public string name { get; set; }

        public TVShowEpisodeToAir next_episode_to_air { get; set; }

        public IEnumerable<TVShowNetworks> networks { get; set; }

        public int number_of_episodes { get; set; }

        public int number_of_seasons { get; set; }

        public IEnumerable<string> origin_country { get; set; }

        public string original_launguage { get; set; }

        public string original_name { get; set; }

        public string overview { get; set; }

        public float popularity { get; set; }

        public string poster_path { get; set; }

        public IEnumerable<ProductionCompanies> production_companies { get; set; }

        public IEnumerable<TVShowSeason> seasons { get; set; }

        public string status { get; set; }

        public string type { get; set; }

        public float vote_average { get; set; }

        public int vote_count { get; set; }

        public TVCredits credits { get; set; }

        public TVRatings content_ratings { get; set; }
    }

    public class TVShowCreatedBy
    {
        public int id { get; set; }

        public string cerdit_id { get; set; }

        public string name { get; set; }

        public int gender { get; set; }

        public string profile_path { get; set; }
    }

    public class TVShowGenre
    {
        public GenreType id { get; set; }

        public string namne { get; set; }
    }

    public class TVShowEpisodeToAir
    {
        public string air_date { get; set; }

        public int episode_number { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public string overview { get; set; }

        public string production_code { get; set; }

        public int season_number { get; set; }

        public int show_id { get; set; }

        public string still_path { get; set; }

        public float vote_average { get; set; }

        public int vote_count { get; set; }
    }

    public class TVShowNetworks
    {
        public string name { get; set; }

        public int id { get; set; }

        public string logo_path { get; set; }

        public string origin_country { get; set; }
    }

    public class TVShowSeason
    {
        public string air_date { get; set; }

        public int episode_count { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public string overview { get; set; }

        public string poster_path { get; set; }

        public int season_number { get; set; }
    }

    public class TVCredits
    {
        public TVCredits()
        {
            cast = new List<CastMember>();
            crew = new List<CrewMember>();
        }
        public IEnumerable<CastMember> cast { get; set; }

        public IEnumerable<CrewMember> crew { get; set; }

        public int id { get; set; }
    }

    public class TVRatings
    {
        public TVRatings()
        {
            results = new List<TVRating>();
        }

        public int id { get; set; }

        public IEnumerable<TVRating> results { get; set; }
    }

    public class TVRating
    {
        public string iso_3166_1 { get; set; }

        public string rating { get; set; }
    }

}
