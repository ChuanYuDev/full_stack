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
    
    model: ActorDTO = {
        id: 1, 
        name: "Tom Hanks", 
        dateOfBirth: new Date("1990-3-4"),
        picture: "https://upload.wikimedia.org/wikipedia/commons/thumb/3/39/TomHanksPrincEdw031223_%2811_of_41%29_%28cropped%29.jpg/500px-TomHanksPrincEdw031223_%2811_of_41%29_%28cropped%29.jpg",
    }
    
    saveChanges(actor: ActorCreationDTO) {
        console.log("Edit Actor", actor);
    }
}
