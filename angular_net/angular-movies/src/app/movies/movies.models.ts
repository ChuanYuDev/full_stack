export interface MovieDTO {
    id: number;
    title: string;
    releaseDate?: Date;
    trailer: string;
    poster?: string;
    genresIds?: number[];
    theatersIds?: number[];
}

export interface MovieCreationDTO {
    title: string;
    releaseDate?: Date;
    trailer: string;
    poster?: File | string;
    genresIds?: number[];
    theatersIds?: number[];
}