import {Component, Input} from '@angular/core';
import {GenericListComponent} from "../../shared/components/generic-list/generic-list.component";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MovieDto} from "../movies.models";
import {RouterLink} from "@angular/router";

@Component({
    selector: 'app-movies-list',
    imports: [GenericListComponent, MatButtonModule, MatIconModule, RouterLink],
    templateUrl: './movies-list.component.html',
    styleUrl: './movies-list.component.css'
})
export class MoviesListComponent {
    // @Input({required: true})
    // movies?: MovieDto[];
    @Input({required: true})
    movies?: any[];
    
    removeMovie(movie: any){
        let index = this.movies?.findIndex((m: any) => m.title === movie.title);
        
        if (typeof index !== "undefined") {
            this.movies?.splice(index,  1);
        }
    }
}
