using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;

namespace MarksMovies.Models
{
    public enum DiscType {
        [Display(Name = "DVD")]
        DVD = 0,
        [Display(Name = "Blu-ray")]
        Bluray = 1,
        [Display(Name = "4K UHD Blu-ray")]
        UHD_Bluray = 2
    }


    public enum Rating {
        [Display(Name = "G")]
        G = 0,
        [Display(Name = "PG")]
        PG = 1,
        [Display(Name = "PG-13")]
        PG13 = 2,
        [Display(Name = "R")]
        R = 3,
        [Display(Name = "TV-Y")]
        TV_Y = 5,
        [Display(Name = "TV-Y7")]
        TV_Y7 = 6,
        [Display(Name = "TV-G")]
        TV_G = 10,
        [Display(Name = "TV-PG")]
        TV_PG = 7,
        [Display(Name = "TV-14")]
        TV_14 = 8,
        [Display(Name = "TV-MA")]
        TV_MA = 9,
        [Display(Name = "Unrated")]
        Unrated = 4
    }

    public enum MovieOrTVShow
    {
        [Display(Name = "Movie")]
        Movie = 0,
        [Display(Name ="TV Show")]
        TV = 1
    }

    public class Movie
    {
        public Movie()
        {
            Genres = new List<Genre>();
            Year = 1900;
        }

        public int ID { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        public IList<Genre> Genres { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Rating Rating { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [Display(Name = "Media Type")]
        public DiscType MediaType { get; set; }

        [Display(Name = "IMDB ID")]
        [StringLength(9, MinimumLength =9)]
        [RegularExpression(@"^tt[0-9]{7}$")]
        public string IMDB_ID { get; set; }

        public int Rank { get; set; }

        public int Year { get; set; }

        [StringLength(500, MinimumLength = 3)]
        public string Comments { get; set; }

        public int TMDB_ID { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [Display(Name = "Movie or Television?")]
        public MovieOrTVShow MovieOrTVShow { get; set; }

        public int Season { get; set; }

        public static Rating RatingFromString(string rating)
        {
            switch (rating)
            {
                case "G":
                    return Rating.G;
                case "PG":
                    return Rating.PG;
                case "PG-13":
                    return Rating.PG13;
                case "R":
                    return Rating.R;
                case "TV-Y":
                    return Rating.TV_Y;
                case "TV-Y7":
                    return Rating.TV_Y7;
                case "TV-G":
                    return Rating.TV_G;
                case "TV-PG":
                    return Rating.TV_PG;
                case "TV-14":
                    return Rating.TV_14;
                case "TV-MA":
                    return Rating.TV_MA;
                case "NR":
                    return Rating.Unrated;
                default:
                    return Rating.Unrated;
            }
        }

        public string GetGenresAsString()
        {
            string genreText = string.Empty;
            
            for (var i = 0; i < this.Genres.Count; i++)
            {
                genreText += EnumHelper<GenreType>.GetDisplayValue(this.Genres[i].genre);
                if(i < this.Genres.Count - 1)
                    genreText += ", ";
            }
            return genreText;
            
        }



    }


}