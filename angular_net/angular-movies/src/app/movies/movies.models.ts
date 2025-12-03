import {ActorAutoCompleteDTO} from "../actors/actors.models";

export interface MovieDTO {
    id: number;
    title: string;
    releaseDate?: Date;
    trailer: string;
    poster?: string;
    genresIds?: number[];
    theatersIds?: number[];
    actors?: ActorAutoCompleteDTO[];
}

export interface MovieCreationDTO {
    title: string;
    releaseDate?: Date;
    trailer: string;
    poster?: File | string;
    genresIds?: number[];
    theatersIds?: number[];
    actors?: ActorAutoCompleteDTO[];
}