// Interface
//      It provides a way to describe the shape of objects, including their properties and methods, without implementing any functionality
//      Interfaces solely focus on the structure and type-checking aspects, allowing for better code understanding and validation during development
//
// DTO
//      A Data Transfer Object is an object that is used to encapsulate data, and send it from one subsystem of an application to another
//
// CreationDTO
//      Have the information necessary for creating or updating an entity
//
// First letter is uppercase for interface
export interface GenreCreationDTO {
    name: string;
}

// GenreDTO
//      We use for reading information from our API
export interface GenreDTO {
    id: number;
    name: string;
}