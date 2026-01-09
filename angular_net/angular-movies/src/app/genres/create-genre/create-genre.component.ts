import {Component, inject} from '@angular/core';
import {Router} from "@angular/router";
import {GenreCreationDTO} from "../genres.models";
import {GenresFormComponent} from "../genres-form/genres-form.component";
import {GenresService} from "../genres.service";
import {extractErrors} from "../../shared/functions/extractErrors";
import {DisplayErrorsComponent} from "../../shared/components/display-errors/display-errors.component";

@Component({
    selector: 'app-create-genre',
    imports: [
        GenresFormComponent,
        DisplayErrorsComponent
    ],
    templateUrl: './create-genre.component.html',
    styleUrl: './create-genre.component.css'
})
export class CreateGenreComponent {
    
    router = inject(Router);
    genresService = inject(GenresService);
    errors: string[] = [];
    
    saveChanges(genre: GenreCreationDTO) {
        this.genresService.create(genre).subscribe({
            next: () => {
                this.router.navigate(["/genres"]);
            },
            error: err => {
                this.errors = extractErrors(err);
            }
        });
    }
}