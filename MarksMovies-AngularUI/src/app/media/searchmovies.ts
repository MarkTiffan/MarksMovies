import { GenreType } from './genre';

export interface ISearchMovies
{
    // This class maps to the TMDB API call for /search/movie
    page: number;
    total_pages: number;
    total_results: number;
    results: ISearchMoviesResult[];
}

export interface ISearchMoviesResult
{
    poster_path: string;
    adult: boolean;
    overview: string;
    release_date: string;
    genre_ids: GenreType[];
    id: number;
    original_title: string;
    original_language: string;
    title: string;
    backdrop_path: string;
    popularity: number;
    vote_count: number;
    video: boolean;
    vote_average: number;
}

