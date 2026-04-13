import {Component, inject, Input, OnInit, ViewChild} from '@angular/core';
import {ActorAutoCompleteDto} from "../actors.models";
import {FormControl, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatAutocompleteModule, MatAutocompleteSelectedEvent} from "@angular/material/autocomplete";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatTable, MatTableModule} from "@angular/material/table";
import {MatIconModule} from "@angular/material/icon";
import {CdkDragDrop, DragDropModule, moveItemInArray} from "@angular/cdk/drag-drop";
import {ActorsService} from "../actors.service";
import {ImageComponent} from "../../shared/components/image/image.component";

@Component({
    selector: 'app-actors-autocomplete',
    imports: [MatAutocompleteModule, MatFormFieldModule, ReactiveFormsModule, MatInputModule, MatTableModule, FormsModule, MatIconModule, DragDropModule, ImageComponent],
    templateUrl: './actors-autocomplete.component.html',
    styleUrl: './actors-autocomplete.component.css'
})
export class ActorsAutocompleteComponent implements OnInit{
    
    actorsService = inject(ActorsService);

    actors: ActorAutoCompleteDto[] = [];

    @Input({required: true})
    selectedActors: ActorAutoCompleteDto[] = [];

    control = new FormControl("");

    columnsToDisplay = ["image", "name", "character", "actions"];

    @ViewChild(MatTable)
    table?: MatTable<ActorAutoCompleteDto>;

    ngOnInit() {
        this.control.valueChanges.subscribe(value => {
            console.log(value);
            
            if (typeof value === "string" && value) {
                this.actorsService.getByName(value).subscribe(actors => {
                    this.actors = actors;
                });
            }
        });
    }

    handleSelection(event: MatAutocompleteSelectedEvent) {
        this.selectedActors.push(event.option.value);

        this.control.patchValue("");

        this.table?.renderRows();
    }

    handleDrop(event: CdkDragDrop<any[]>){
        const previousIndex = this.selectedActors.findIndex(actor => actor === event.item.data);

        moveItemInArray(this.selectedActors, previousIndex, event.currentIndex);

        this.table?.renderRows();
    }

    delete(value: ActorAutoCompleteDto) {
        const index = this.selectedActors.findIndex(actor => actor.id == value.id);

        this.selectedActors.splice(index, 1);

        this.table?.renderRows();
    }
}
