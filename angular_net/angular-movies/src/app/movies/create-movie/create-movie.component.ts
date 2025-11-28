import { Component } from '@angular/core';
import {MoviesFormComponent} from "../movies-form/movies-form.component";
import {MovieCreationDTO} from "../movies.models";
import {MultipleSelectorDTO} from "../../shared/components/multiple-selector/multiple-selector.model";

@Component({
    selector: 'app-create-movie',
    imports: [
        MoviesFormComponent
    ],
    templateUrl: './create-movie.component.html',
    styleUrl: './create-movie.component.css'
})
export class CreateMovieComponent {
    selectedGenres: MultipleSelectorDTO[] = [];
    nonSelectedGenres: MultipleSelectorDTO[] = [
        {key: 1, description: "Comedy"},
        {key: 2, description: "Action"},
        {key: 3, description: "Drama"},
    ];

    saveChanges(movie: MovieCreationDTO): void {
        console.log("Create movie", movie);
    }
}
