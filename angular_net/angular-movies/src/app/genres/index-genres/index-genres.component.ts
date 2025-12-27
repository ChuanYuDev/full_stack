import {Component, inject} from '@angular/core';
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {RouterLink} from "@angular/router";
import {GenresService} from "../genres.service";
import {GenreDTO} from "../genres.models";

@Component({
    selector: 'app-index-genres',
    imports: [MatButtonModule, MatIconModule, RouterLink],
    templateUrl: './index-genres.component.html',
    styleUrl: './index-genres.component.css'
})
export class IndexGenresComponent {
    genresService = inject(GenresService);
    genres: GenreDTO[] = [];
    
    constructor() {
        this.genresService.getAll().subscribe(genres => {
            this.genres = genres;
        });
    }
}
