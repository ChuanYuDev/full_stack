import {Component, inject, model} from '@angular/core';
import {MoviesFormComponent} from "../movies-form/movies-form.component";
import {MovieCreationDto} from "../movies.models";
import {MultipleSelectorDto} from "../../shared/components/multiple-selector/multiple-selector.model";
import {ActorAutoCompleteDto} from "../../actors/actors.models";
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
    
    selectedGenres: MultipleSelectorDto[] = [];
    nonSelectedGenres: MultipleSelectorDto[] = [];

    selectedTheaters: MultipleSelectorDto[] = [];
    nonSelectedTheaters: MultipleSelectorDto[] = [];

    selectedActors: ActorAutoCompleteDto[] = [];
    
    constructor() {
        this.moviesService.postGet().subscribe(moviePostGetDto => {
            this.nonSelectedGenres = moviePostGetDto.genres.map(genre =>
                <MultipleSelectorDto>{key: genre.id, description: genre.name}
            );
            
            this.nonSelectedTheaters = moviePostGetDto.theaters.map(theater => 
                <MultipleSelectorDto>{key: theater.id, description: theater.name}
            );
        });   
    }

    saveChanges(movie: MovieCreationDto): void {
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
