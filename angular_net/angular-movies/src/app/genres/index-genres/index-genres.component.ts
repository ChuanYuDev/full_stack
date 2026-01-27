import {Component, inject} from '@angular/core';
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {RouterLink} from "@angular/router";
import {GenresService} from "../genres.service";
import {GenreDTO} from "../genres.models";
import {GenericListComponent} from "../../shared/components/generic-list/generic-list.component";
import {MatTableModule} from "@angular/material/table";

@Component({
    selector: 'app-index-genres',
    imports: [MatButtonModule, MatIconModule, RouterLink, GenericListComponent, MatTableModule],
    templateUrl: './index-genres.component.html',
    styleUrl: './index-genres.component.css'
})
export class IndexGenresComponent {
    genresService = inject(GenresService);
    genres!: GenreDTO[];
    columnsToDisplay = ["id", "name", "actions"];
    
    constructor() {
        this.genresService.getAll().subscribe(genres => {
            this.genres = genres;
        });
    }
}
