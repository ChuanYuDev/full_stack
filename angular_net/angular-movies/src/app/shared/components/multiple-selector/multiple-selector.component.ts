import {Component, Input} from '@angular/core';
import {MultipleSelectorDto} from "./multiple-selector.model";

@Component({
    selector: 'app-multiple-selector',
    imports: [],
    templateUrl: './multiple-selector.component.html',
    styleUrl: './multiple-selector.component.css'
})
export class MultipleSelectorComponent {
    @Input({required: true})
    selected: MultipleSelectorDto[] = [];

    @Input({required: true})
    nonSelected: MultipleSelectorDto[] = [];

    select(element: MultipleSelectorDto, index: number) {
        this.selected.push(element);
        this.nonSelected.splice(index, 1);
    }

    deselect(element: MultipleSelectorDto, index: number) {
        this.nonSelected.push(element);
        this.selected.splice(index, 1);
    }

    selectAll() {
        this.selected.push(...this.nonSelected);
        this.nonSelected.length = 0;
    }

    deselectAll() {
        this.nonSelected.push(...this.selected);
        this.selected.length = 0;
    }

}
