import {Component, Input, numberAttribute} from '@angular/core';
import {ActorsFormComponent} from "../actors-form/actors-form.component";
import {ActorCreationDTO, ActorDTO} from "../actors.models";

@Component({
    selector: 'app-edit-actor',
    imports: [
        ActorsFormComponent
    ],
    templateUrl: './edit-actor.component.html',
    styleUrl: './edit-actor.component.css'
})
export class EditActorComponent {
    @Input({transform: numberAttribute})
    id!: number;
    
    model: ActorDTO = {id: 1, name: "Tom Hanks", dateOfBirth: new Date("1990-3-4"),}
    
    saveChanges(actor: ActorCreationDTO) {
        console.log("Edit Actor", actor);
    }
}
