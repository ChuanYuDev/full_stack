import {Component, Input, numberAttribute} from '@angular/core';
import {TheaterCreationDTO, TheaterDTO} from "../theaters.models";
import {TheatersFormComponent} from "../theaters-form/theaters-form.component";

@Component({
    selector: 'app-edit-theater',
    imports: [
        TheatersFormComponent
    ],
    templateUrl: './edit-theater.component.html',
    styleUrl: './edit-theater.component.css'
})
export class EditTheaterComponent {
    @Input({transform: numberAttribute})
    id!: number;
    
    model: TheaterDTO = {id: 1, name: "Star cinema"};
    
    saveChanges(theater: TheaterCreationDTO) {
        console.log("Edit theater", theater);
    }
}
