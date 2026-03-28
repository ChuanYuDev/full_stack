import {Component, inject, model} from '@angular/core';
import {MoviesFormComponent} from "../movies-form/movies-form.component";
import {MovieCreationDTO} from "../movies.models";
import {MultipleSelectorDTO} from "../../shared/components/multiple-selector/multiple-selector.model";
import {ActorAutoCompleteDTO} from "../../actors/actors.models";
import {MoviesService} from "../movies.service";
import {Router} from "@angular/router";
import {extractErrors} from "../../shared/functions/extractErrors";
import {DisplayErrorsComponent} from "../../shared/components/display-errors/display-errors.component";
import {LoadingComponent} from "../../shared/components/loading/loading.component";

@Component({
    selector: 'app-create-movie',
    imports: [MoviesFormComponent, DisplayErrorsComponent, LoadingComponent],
    templateUrl: './create-movie.component.html',
    styleUrl: './create-movie.component.css'
})
export class CreateMovieComponent {
    moviesService = inject(MoviesService);
    router = inject(Router);
    errors: string[] = [];
    
    selectedGenres: MultipleSelectorDTO[] = [];
    nonSelectedGenres: MultipleSelectorDTO[] = [];

    selectedTheaters: MultipleSelectorDTO[] = [];
    nonSelectedTheaters: MultipleSelectorDTO[] = [];

    selectedActors: ActorAutoCompleteDTO[] = [];
    
    constructor() {
        this.moviesService.postGet().subscribe(model => {
            this.nonSelectedGenres = model.genres.map(genre =>
                <MultipleSelectorDTO>{key: genre.id, description: genre.name}
            );
            
            this.nonSelectedTheaters = model.theaters.map(theater => 
                <MultipleSelectorDTO>{key: theater.id, description: theater.name}
            );
        });   
    }

    saveChanges(movie: MovieCreationDTO): void {
        this.moviesService.post(movie).subscribe({
            next: () => {
                this.router.navigate(["/"]);
            },
            error: err => {
                this.errors = extractErrors(err);
            }
        });
    }
}
