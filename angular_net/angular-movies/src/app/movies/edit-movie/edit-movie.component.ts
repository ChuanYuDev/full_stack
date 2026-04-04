import {Component, Input, numberAttribute} from '@angular/core';
import {MovieCreationDto, MovieDto} from "../movies.models";
import {MoviesFormComponent} from "../movies-form/movies-form.component";
import {MultipleSelectorDto} from "../../shared/components/multiple-selector/multiple-selector.model";
import {ActorAutoCompleteDto} from "../../actors/actors.models";

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

    model: MovieDto = {
        id: 1,
        title: "Spider-Man: Far From Home",
        releaseDate: new Date("2019-08-04"),
        trailer: "absd",
        poster: "https://upload.wikimedia.org/wikipedia/en/b/bd/Spider-Man_Far_From_Home_poster.jpg",
    };

    selectedGenres: MultipleSelectorDto[] = [
        {key: 2, description: "Action"},
    ];
    nonSelectedGenres: MultipleSelectorDto[] = [
        {key: 1, description: "Comedy"},
        {key: 3, description: "Drama"},
    ];

    selectedTheaters: MultipleSelectorDto[] = [
        {key: 1, description: "Star cinema"},
    ];
    nonSelectedTheaters: MultipleSelectorDto[] = [
        {key: 2, description: "Palace ifc"}
    ];

    selectedActors: ActorAutoCompleteDto[] = [
        {id: 3, name: 'Samuel L. Jackson', character: 'Nick Fury', picture: 'https://upload.wikimedia.org/wikipedia/commons/thumb/2/29/SamuelLJackson.jpg/250px-SamuelLJackson.jpg' }
    ];

    saveChanges(movie: MovieCreationDto) {
        console.log("Edit movie", movie);
    }
}
