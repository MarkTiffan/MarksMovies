import { GenreType } from './genre';

export enum ReleaseDateType
    {
        Premiere = 1,
        TheatricalLimited = 2,
        Theatrical = 3,
        Digital = 4,
        Physical = 5,
        TV = 6
    }
export interface IMovieDetails
{

    adult: boolean;
    backdrop_path: string;
    belongs_to_collection: IBelongsToCollection;
    budget: number;
    genres: IMovieDetailGenre[];
    homepage: string;
    id: number;
    imdb_id: string;
    original_language: string;
    original_title: string;
    overview: string;
    popularity: number;
    poster_path: string;
    production_companies: IProductionCompanies[];
    production_countries: IProductionCountries[];
    release_date: string;
    revenue: number;
    runtime: number;
    spoken_languages: ISpokenLanguages[];
    status: string;
    tagline: string;
    title: string;
    video: boolean;
    vote_average: number;
    vote_count: number;
    release_dates: IReleaseDates[];
    credits: IMovieCredits[];
}

export interface IMovieDetailGenre
{
    id: GenreType;
    name: string;
}

export interface IBelongsToCollection
{
    id: number;
    name: string;
    poster_path: string;
    backdrop_path: string;
}

export interface IProductionCompanies
{
    name: string;
    id: number;
    logo_path: string;
    origin_country: string;
}

export interface IProductionCountries
{
    iso_3166_1: string;
    name: string;
}

export interface ISpokenLanguages
{
    iso_639_1: string;
    name: string;
}


export interface IReleaseDates
{
    id: number;
    results: IReleaseDateResults[];
}

export interface IReleaseDateResults
{
    iso_3166_1: string;
    release_dates: IReleaseDate[];
}

export interface IReleaseDate
{
    certification: string;
    iso_639_1: string;
    release_date: string;
    type: ReleaseDateType;
    note: string;
}


export interface IMovieCredits
{
    id: number;
    cast: ICastMember[];
    crew: ICrewMember[];
}


export interface ICastMember
{
    cast_id: number;
    character: string;
    credit_id: string;
    gender: number;
    id: number;
    name: string;
    order: number;
    profile_path: string;
}

export interface ICrewMember
{
    credit_id: string;
    department: string;
    gender: number;
    id: number;
    job: string;
    name: string;
    profile_path: string;
}


