export enum GenreType
    {
        All = "All",
        Action = "Action",
        ActionAdventure = "ActionAdventure",
        Adventure = "Adventure",
        Animation = "Animation",
        Comedy = "Comedy",
        Crime = "Crime",
        Documentary = "Documentary",
        Drama = "Drama",
        Family = "Family",
        Fantasy = "Fantasy",
        History = "History",
        Horror = "Horror",
        Kids = "Kids",
        Music = "Music",
        Mystery = "Mystery",
        News = "News",
        Reality = "Reality",
        Romance = "Romance",
        SciFi = "SciFi",
        SciFiFantasy = "SciFiFantasy",
        Soap = "Soap",
        Talk = "Talk",
        Thriller = "Thriller",
        TV = "TV",
        War = "War",
        WarPolitics = "WarPolitics",
        Western = "Western"
    }

export interface IGenre{
    genre: GenreType;
    // constructor(g: GenreType) {
    //     this.genre = g;
    // }
    // GenreID: number;
    // MovieID: number;

    
}