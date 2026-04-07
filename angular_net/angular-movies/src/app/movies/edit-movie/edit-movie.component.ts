import {Component, inject, Input, numberAttribute, OnInit} from '@angular/core';
import {MovieCreationDto, MovieDto} from "../movies.models";
import {MoviesFormComponent} from "../movies-form/movies-form.component";
import {MultipleSelectorDto} from "../../shared/components/multiple-selector/multiple-selector.model";
import {ActorAutoCompleteDto} from "../../actors/actors.models";
import {MoviesService} from "../movies.service";
import {Router} from "@angular/router";
import {DisplayErrorsComponent} from "../../shared/components/display-errors/display-errors.component";
import {LoadingComponent} from "../../shared/components/loading/loading.component";
import {extractErrors} from "../../shared/functions/extractErrors";

@Component({
    selector: 'app-edit-movie',
    imports: [
        MoviesFormComponent,
        DisplayErrorsComponent,
        LoadingComponent
    ],
    templateUrl: './edit-movie.component.html',
    styleUrl: './edit-movie.component.css'
})
export class EditMovieComponent implements OnInit{
    moviesService = inject(MoviesService);
    router = inject(Router);
    
    errors: string[] = [];
    movieDto?: MovieDto;

    selectedGenres: MultipleSelectorDto[] = [];
    nonSelectedGenres: MultipleSelectorDto[] = [];
    selectedTheaters: MultipleSelectorDto[] = [];
    nonSelectedTheaters: MultipleSelectorDto[] = [];
    selectedActors: ActorAutoCompleteDto[] = [];
    
    @Input({transform: numberAttribute})
    id: number = 0;

    ngOnInit() {
        this.moviesService.putGet(this.id).subscribe(moviePutGetDto => {
            this.movieDto = moviePutGetDto.movie;
            this.selectedGenres = moviePutGetDto.selectedGenres.map(genreDto => <MultipleSelectorDto>{key: genreDto.id, description: genreDto.name});
            this.nonSelectedGenres = moviePutGetDto.nonSelectedGenres.map(genreDto => <MultipleSelectorDto>{key: genreDto.id, description: genreDto.name});
            this.selectedTheaters = moviePutGetDto.selectedTheaters.map(theaterDto => <MultipleSelectorDto>{key: theaterDto.id, description: theaterDto.name});
            this.nonSelectedTheaters = moviePutGetDto.nonSelectedTheaters.map(theaterDto => <MultipleSelectorDto>{key: theaterDto.id, description: theaterDto.name});
            this.selectedActors = moviePutGetDto.actors;
        });
    }

    saveChanges(movieCreationDto: MovieCreationDto) {
        this.moviesService.update(this.id, movieCreationDto).subscribe({
            next: () => {
                this.router.navigate(["/"]);
            },
            error: err => {
                this.errors = extractErrors(err);
            }
        });
    }
}
