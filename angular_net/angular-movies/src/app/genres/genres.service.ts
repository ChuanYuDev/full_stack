import { Injectable } from '@angular/core';
import {GenreDTO} from "./genres.models";

@Injectable({
    providedIn: 'root'
})
export class GenresService {
    constructor() { }
    
    getAll(): GenreDTO[] {
        return [{id: 1, name: "Drama"}];
    }
}
