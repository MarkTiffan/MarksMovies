import { IGenre } from './genre';
import { stringify } from 'querystring';

export enum DiscType {       
    DVD = "DVD",
    Bluray = "Bluray",
    UHD_Bluray = "UHD_Bluray"
}

export enum Rating {
    G = "G",
    PG = "PG",
    PG13 = "PG13",
    R = "R",
    TV_Y = "TV_Y",
    TV_Y7 = "TV_Y7",
    TV_G = "TV_G",
    TV_PG = "TV_PG",
    TV_14 = "TV_14",
    TV_MA = "TV_MA",
    Unrated = "Unrated"
}

export enum MovieOrTVShow{
    Movie = "Movie",
    TV = "TV"
}

export interface IMedia {
    id: number;
    title: string;
    genres: IGenre[];
    rating: Rating;
    mediaType: DiscType;
    imdB_id?: string;
    rank: number;
    year: number;
    comments?: string;
    tmdB_ID: number;
    movieOrTVShow: MovieOrTVShow;
    season?: number;

}