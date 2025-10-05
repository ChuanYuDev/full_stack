import {Component, EventEmitter, Input, Output} from '@angular/core';
import {MatIconModule} from "@angular/material/icon";
import {NgClass} from "@angular/common";

@Component({
    selector: 'app-rating',
    imports: [MatIconModule, NgClass],
    templateUrl: './rating.component.html',
    styleUrl: './rating.component.css'
})
export class RatingComponent {
    @Input({required: true, transform: (value: number) => Array(value).fill(0)})
    maxRating!: any[];
    
    selectedRating: number = 0;
    clickedRating: number = 0;
    
    @Output()
    rated = new EventEmitter<number>();
    
    handleMouseEnter(index: number){
        this.selectedRating = index + 1;
    }
    
    handleMouseLeave() {
        if (this.clickedRating !== 0) {
            this.selectedRating = this.clickedRating;
        } else {
            this.selectedRating = 0;
        }
    }
    
    handleClick(index: number) {
        this.clickedRating = index + 1;
        this.selectedRating = this.clickedRating;
        this.rated.emit(this.selectedRating);
    }
}
