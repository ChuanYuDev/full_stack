import { Component } from '@angular/core';
import {TheatersFormComponent} from "../theaters-form/theaters-form.component";
import {TheaterCreationDTO} from "../theaters.models";

@Component({
    selector: 'app-create-theater',
    imports: [TheatersFormComponent],
    templateUrl: './create-theater.component.html',
    styleUrl: './create-theater.component.css'
})
export class CreateTheaterComponent {
    saveChanges(theater: TheaterCreationDTO) {
        console.log("Create theater", theater);
    }
}
