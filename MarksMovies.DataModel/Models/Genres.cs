using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarksMovies.Models
{
    // these genre values match those from TMDB
    public enum GenreType
    {
        [Display(Name = "Action")]
        Action = 28,
        [Display(Name = "Action & Adventure")]
        ActionAdventure = 10759,
        [Display(Name = "Adventure")]
        Adventure = 12,
        [Display(Name = "Animation")]
        Animation = 16,
        [Display(Name ="Comedy")]
        Comedy = 35,
        [Display(Name = "Crime")]
        Crime = 80,
        [Display(Name = "Documentary")]
        Documentary = 99,
        [Display(Name = "Drama")]
        Drama = 18,
        [Display(Name = "Family")]
        Family = 10751,
        [Display(Name = "Fantasy")]
        Fantasy = 14,
        [Display(Name = "History")]
        History = 36,
        [Display(Name = "Horror")]
        Horror = 27,
        [Display(Name = "Kids")]
        Kids = 10672,
        [Display(Name = "Music")]
        Music = 10402,
        [Display(Name = "Mystery")]
        Mystery = 9648,
        [Display(Name = "News")]
        News = 10763,
        [Display(Name = "Reality")]
        Reality = 10764,
        [Display(Name = "Romance")]
        Romance = 10749,
        [Display(Name = "Science Fiction")]
        SciFi = 878,
        [Display(Name ="Sci-Fi & Fantasy")]
        SciFiFantasy = 10765,
        [Display(Name = "Soap")]
        Soap = 10766,
        [Display(Name = "Talk")]
        Talk = 10767,
        [Display(Name = "Thriller")]
        Thriller = 53,
        [Display(Name = "TV")]
        TV = 10770,
        [Display(Name = "War")]
        War = 10752,
        [Display(Name ="War & Politics")]
        WarPolitics = 10768,
        [Display(Name = "Western")]
        Western = 37
    }
    public class Genre
    {
        public Genre(GenreType g)
        {
            genre = g;
        }
        public Genre() { }

        [JsonIgnore]
        public int GenreID { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GenreType genre { get; set; }

        [JsonIgnore]
        public int MovieID { get; set; }

        [ForeignKey("MovieID")]
        [JsonIgnore]
        public Movie Movie { get; set; }
    }
}

