import {Component, inject, Input, numberAttribute, OnInit} from '@angular/core';
import {ActorsFormComponent} from "../actors-form/actors-form.component";
import {ActorCreationDTO, ActorDTO} from "../actors.models";
import {ActorsService} from "../actors.service";
import {Router} from "@angular/router";
import {extractErrors} from "../../shared/functions/extractErrors";
import {DisplayErrorsComponent} from "../../shared/components/display-errors/display-errors.component";
import {LoadingComponent} from "../../shared/components/loading/loading.component";

@Component({
    selector: 'app-edit-actor',
    imports: [ActorsFormComponent, DisplayErrorsComponent, LoadingComponent],
    templateUrl: './edit-actor.component.html',
    styleUrl: './edit-actor.component.css'
})
export class EditActorComponent implements OnInit{
    actorsService = inject(ActorsService);
    router = inject(Router);
    
    @Input({transform: numberAttribute})
    id: number = 0;
    
    model?: ActorDTO
    errors: string[] = [];

    ngOnInit(): void {
        this.actorsService.getById(this.id).subscribe((actor: ActorDTO) => {
            this.model = actor;
        });
    }
    
    saveChanges(actor: ActorCreationDTO) {
        this.actorsService.update(this.id, actor).subscribe({
            next: () => {
                this.router.navigate(["/actors"]);       
            },
            
            error: err => {
                this.errors = extractErrors(err);
            }
        });
    }
}
