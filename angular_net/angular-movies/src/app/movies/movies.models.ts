import {ActorAutoCompleteDto} from "../actors/actors.models";
import {GenreDto} from "../genres/genres.models";
import {TheaterDto} from "../theaters/theaters.models";

export interface MovieDto {
    id: number;
    title: string;
    releaseDate?: Date;
    trailer?: string;
    poster?: string;
    // genresIds?: number[];
    // theatersIds?: number[];
    // actors?: ActorAutoCompleteDto[];
}

export interface MovieCreationDto {
    title: string;
    releaseDate?: Date;
    trailer?: string;
    poster?: File | string;
    genresIds?: number[];
    theatersIds?: number[];
    actors?: ActorAutoCompleteDto[];
}

export interface MoviePostGetDto {
    genres: GenreDto[];
    theaters: TheaterDto[];
}

export interface LandingDto {
    inTheaters: MovieDto[];
    upcomingReleases: MovieDto[];
}