import { Component, Input, numberAttribute } from '@angular/core';

@Component({
    selector: 'app-edit-movie',
    imports: [],
    templateUrl: './edit-movie.component.html',
    styleUrl: './edit-movie.component.css'
})
export class EditMovieComponent {

    // numberAttribute
    //      Transform a value (typically a string) to a number
    //
    // id
    //      Is the same as :id in route configuration
    @Input({transform: numberAttribute})
    id!: number;
}
