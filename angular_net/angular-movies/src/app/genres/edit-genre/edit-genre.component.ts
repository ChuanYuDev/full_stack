import {Component, inject, Input, numberAttribute, OnInit} from '@angular/core';
import {GenreCreationDTO, GenreDTO} from "../genres.models";
import {GenresFormComponent} from "../genres-form/genres-form.component";
import {GenresService} from "../genres.service";
import {Router} from "@angular/router";
import {extractErrors} from "../../shared/functions/extractErrors";
import {DisplayErrorsComponent} from "../../shared/components/display-errors/display-errors.component";
import {LoadingComponent} from "../../shared/components/loading/loading.component";

@Component({
    selector: 'app-edit-genre',
    imports: [GenresFormComponent, DisplayErrorsComponent, LoadingComponent],
    templateUrl: './edit-genre.component.html',
    styleUrl: './edit-genre.component.css'
})
export class EditGenreComponent implements OnInit{
    genresService = inject(GenresService);
    router = inject(Router);
    
    @Input({transform: numberAttribute})
    id: number = 0;
    
    model?: GenreDTO;
    errors: string[] = [];

    ngOnInit(): void {
        this.genresService.getById(this.id).subscribe(genre => {
            this.model = genre;
        });
    }
    
    saveChanges(genre: GenreCreationDTO) {
        this.genresService.update(this.id, genre).subscribe({
            next: () => {
                this.router.navigate(["/genres"]);
            },
            error: (err) => {
                this.errors = extractErrors(err);
            }
        });
    }
}
