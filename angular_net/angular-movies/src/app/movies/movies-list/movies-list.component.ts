import {Component, EventEmitter, inject, Input, Output} from '@angular/core';
import {GenericListComponent} from "../../shared/components/generic-list/generic-list.component";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MovieDto} from "../movies.models";
import {RouterLink} from "@angular/router";
import {SwalDirective} from "@sweetalert2/ngx-sweetalert2";
import {MoviesService} from "../movies.service";
import {ImageComponent} from "../../shared/components/image/image.component";

@Component({
    selector: 'app-movies-list',
    imports: [GenericListComponent, MatButtonModule, MatIconModule, RouterLink, SwalDirective, ImageComponent],
    templateUrl: './movies-list.component.html',
    styleUrl: './movies-list.component.css'
})
export class MoviesListComponent {
    moviesService = inject(MoviesService);
    
    swalParams = {
        title: "Confirmation",
        text: "Are you sure you want to delete this movie?",
        showCancelButton: true,
    };
    
    // @Input({required: true})
    // movies?: MovieDto[];
    @Input({required: true})
    movies?: any[];
    
    @Output()
    deleted = new EventEmitter<void>();
    
    delete(id: number) {
        this.moviesService.delete(id).subscribe(() => {
            this.deleted.emit();       
        });
    }
}
