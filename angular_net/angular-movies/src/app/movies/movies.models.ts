import {ActorAutoCompleteDto} from "../actors/actors.models";
import {GenreDto} from "../genres/genres.models";
import {TheaterDto} from "../theaters/theaters.models";
import {PaginationDTO} from "../shared/models/pagination.model";

export interface MovieDto {
    id: number;
    title: string;
    releaseDate?: Date;
    trailer?: string;
    poster?: string;
}

export interface MovieDetailsDto extends MovieDto{
    genres?: GenreDto[];
    theaters?: TheaterDto[];
    actors?: ActorAutoCompleteDto[];
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

export interface LandingDto {
    inTheaters: MovieDto[];
    upcomingReleases: MovieDto[];
}

export interface MoviePostGetDto {
    genres: GenreDto[];
    theaters: TheaterDto[];
}

export interface MoviePutGetDto {
    movie: MovieDto;
    selectedGenres: GenreDto[];
    nonSelectedGenres: GenreDto[];
    selectedTheaters: TheaterDto[];
    nonSelectedTheaters: TheaterDto[];
    actors: ActorAutoCompleteDto[];
}

export interface MoviesSearchDto {
    title?: string;
    genreId?: number;
    upcomingReleases?: boolean;
    inTheaters?: boolean;
}

export interface MoviesSearchWithPaginationDto extends MoviesSearchDto, PaginationDTO {}