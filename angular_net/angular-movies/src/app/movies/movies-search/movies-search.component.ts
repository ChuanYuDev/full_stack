import {Component, inject} from '@angular/core';
import {FormBuilder, ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatButtonModule} from "@angular/material/button";
import {GenreDTO} from "../../genres/genres.models";

@Component({
    selector: 'app-movies-search',
    imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatCheckboxModule, MatButtonModule],
    templateUrl: './movies-search.component.html',
    styleUrl: './movies-search.component.css'
})
export class MoviesSearchComponent {
    private formBuilder = inject(FormBuilder);

    form = this.formBuilder.group({
        title: "",
        genreId: 0,
        upcomingReleases: false,
        inTheaters: false
    });

    genres: GenreDTO[] = [
        {id: 1, name: "Comedy"},
        {id: 2, name: "Action"},
        {id: 3, name: "Drama"},
    ];

    clear() {
        this.form.patchValue({
            title: "",
            genreId: 0,
            upcomingReleases: false,
            inTheaters: false
        });
    }
}
