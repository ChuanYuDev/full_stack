import {Component, Input, numberAttribute} from '@angular/core';
import {MovieCreationDTO, MovieDTO} from "../movies.models";
import {MoviesFormComponent} from "../movies-form/movies-form.component";

@Component({
    selector: 'app-edit-movie',
    imports: [
        MoviesFormComponent
    ],
    templateUrl: './edit-movie.component.html',
    styleUrl: './edit-movie.component.css'
})
export class EditMovieComponent {
    @Input({transform: numberAttribute})
    id!: number;

    model: MovieDTO = {
        id: 1,
        title: "Spider-Man: Far From Home",
        releaseDate: new Date("2019-08-04"),
        trailer: "absd",
        poster: "https://upload.wikimedia.org/wikipedia/en/b/bd/Spider-Man_Far_From_Home_poster.jpg",
    };

    saveChanges(movie: MovieCreationDTO) {
        console.log("Edit movie", movie);
    }
}
