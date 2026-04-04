export interface ActorDto {
    id: number;
    name: string;
    dateOfBirth: Date;
    picture?: string;
}

export interface ActorCreationDto {
    name: string;
    dateOfBirth: Date;
    picture?: File | string;
}

export interface ActorAutoCompleteDto {
    id: number;
    name: string;
    character?: string;
    picture?: string;
}