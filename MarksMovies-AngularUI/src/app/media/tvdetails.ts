import { ICastMember, ICrewMember, IProductionCompanies } from './moviedetails';

export interface ITVShowDetails
{
    backdrop_path: string;
    created_by: ITVShowCreatedBy;
    episode_run_time: number[];
    first_air_date: string;
    genres: ITVShowGenre[];
    homepage: string;
    id: number;
    in_production: boolean;
    languages: string[];
    last_air_date: string;
    last_episode_to_air: ITVShowEpisodeToAir;
    name: string;
    next_episode_to_air: ITVShowEpisodeToAir;
    networks: ITVShowNetworks[];
    number_of_episodes: number;
    number_of_seasons: number;
    origin_country: string[];
    original_launguage: string;
    original_name: string;
    overview: string;
    popularity: number;
    poster_path: string;
    production_companies: IProductionCompanies[];
    seasons: ITVShowSeason[];
    status: string;
    type: string;
    vote_average: number;
    vote_count: number;
    credits: ITVCredits;
    content_ratings: ITVRatings;
}

export interface ITVShowCreatedBy
{
    id: number;
    cerdit_id: string;
    name: string;
    gender: number;
    profile_path: string;
}

export interface ITVShowGenre
{
    id: number;
    namne: string;
}

export interface ITVShowEpisodeToAir
{
    air_date: string;
    episode_number: number;
    id: number;
    name: string;
    overview: string;
    production_code: string;
    season_number: number;
    show_id: number;
    still_path: string;
    vote_average: number;
    vote_count:number;
}

export interface ITVShowNetworks
{
    name: string;
    id: number;
    logo_path: string;
    origin_country: string;
}

export interface ITVShowSeason
{
    air_date: string;
    episode_count: number;
    id: number;
    name: string;
    overview: string;
    poster_path: string;
    season_number: number;
}

export interface ITVCredits
{
    cast: ICastMember[];
    crew: ICrewMember[];
    id: number;
}

export interface ITVRatings
{
    id: number;
    results: ITVRating[];
}

export interface ITVRating
{
    iso_3166_1: string;
    rating: string;
}