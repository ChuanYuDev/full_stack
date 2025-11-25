import { Component } from '@angular/core';
import {MoviesFormComponent} from "../movies-form/movies-form.component";
import {MovieCreationDTO} from "../movies.models";

@Component({
    selector: 'app-create-movie',
    imports: [
        MoviesFormComponent
    ],
    templateUrl: './create-movie.component.html',
    styleUrl: './create-movie.component.css'
})
export class CreateMovieComponent {
    saveChanges(movie: MovieCreationDTO): void {
        console.log("Create movie", movie);
    }
}
