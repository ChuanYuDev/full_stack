import {Component, inject} from '@angular/core';
import {Router} from "@angular/router";
import {GenreCreationDTO} from "../genres.models";
import {GenresFormComponent} from "../genres-form/genres-form.component";

@Component({
    selector: 'app-create-genre',
    imports: [
        GenresFormComponent
    ],
    templateUrl: './create-genre.component.html',
    styleUrl: './create-genre.component.css'
})
export class CreateGenreComponent {
    
    router = inject(Router);
    
    saveChanges(genres: GenreCreationDTO) {
        console.log("Create genre: ", genres);
        this.router.navigate(["/genres"]);
    }
}