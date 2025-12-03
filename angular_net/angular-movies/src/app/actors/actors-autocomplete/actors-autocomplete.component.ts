import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {ActorAutoCompleteDTO} from "../actors.models";
import {FormControl, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatAutocompleteModule, MatAutocompleteSelectedEvent} from "@angular/material/autocomplete";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatTable, MatTableModule} from "@angular/material/table";
import {MatIconModule} from "@angular/material/icon";
import {CdkDragDrop, DragDropModule, moveItemInArray} from "@angular/cdk/drag-drop";

@Component({
    selector: 'app-actors-autocomplete',
    imports: [MatAutocompleteModule, MatFormFieldModule, ReactiveFormsModule, MatInputModule, MatTableModule, FormsModule, MatIconModule, DragDropModule],
    templateUrl: './actors-autocomplete.component.html',
    styleUrl: './actors-autocomplete.component.css'
})
export class ActorsAutocompleteComponent implements OnInit{
    actorsOriginal: ActorAutoCompleteDTO[] = [
        {id: 1, name: 'Tom Holland', character: '', picture: 'https://upload.wikimedia.org/wikipedia/commons/thumb/3/3c/Tom_Holland_by_Gage_Skidmore.jpg/330px-Tom_Holland_by_Gage_Skidmore.jpg'},
        {id: 2, name: 'Tom Hanks', character: '', picture: 'https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Tom_Hanks_TIFF_2019.jpg/220px-Tom_Hanks_TIFF_2019.jpg' },
        {id: 3, name: 'Samuel L. Jackson', character: '', picture: 'https://upload.wikimedia.org/wikipedia/commons/thumb/2/29/SamuelLJackson.jpg/250px-SamuelLJackson.jpg' }
    ];

    actors = this.actorsOriginal;

    @Input({required: true})
    selectedActors: ActorAutoCompleteDTO[] = [];

    control = new FormControl("");

    columnsToDisplay = ["image", "name", "character", "actions"];

    @ViewChild(MatTable)
    table?: MatTable<ActorAutoCompleteDTO>;

    ngOnInit() {
        this.control.valueChanges.subscribe(value => {
            this.actors = this.actorsOriginal;

            if (value) {
                this.actors = this.actors.filter(actor => actor.name.indexOf(value) !== -1)
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

    delete(value: ActorAutoCompleteDTO) {
        const index = this.selectedActors.findIndex(actor => actor.id == value.id);

        this.selectedActors.splice(index, 1);

        this.table?.renderRows();
    }
}
