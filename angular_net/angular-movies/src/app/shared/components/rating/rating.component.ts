import { NgClass } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-rating',
  imports: [MatIconModule, NgClass],
  templateUrl: './rating.component.html',
  styleUrl: './rating.component.css'
})
// export class RatingComponent implements OnInit{
export class RatingComponent{
  // @Input({required: true})
  // maxRating!: number;

  // With a transform function, we are going to be able to receive a number in this input and transform it into an array
  //    Avoid having an extra property
  @Input({required: true, transform: (value: number) => Array(value).fill(0)})
  maxRating!: any[];

  @Input()
  selectedRating = 0;

  // maxRatingArray: any[] = [];

  clickedRating = 0;

  // The EventEmitter is a special class in Angular that allows us to emit events
  //    Type will be number indicating that we want to pass through this event a number which will be the rate of the user
  @Output()
  rated = new EventEmitter<number>();

  // Assign maxRating elements in maxRatingArray
  //    But we cannot use constructor because by the time the constructor is invoked, the maxRating will not be filled
  //    We are going to use a lifecycle event of a component 

  // A lifecycle event
  //    Allows us to execute functionality at a specific times of the lifecycle of a component
  //    One of those life cycles is onInit

  // onInit
  //    Allows us to execute a function after the inputs of the component are present
  // ngOnInit(): void {
  //   this.maxRatingArray = Array(this.maxRating).fill(0);
  // }

  // Get executed when the user puts the cursor of the mouse over a star
  handleMouseEnter(index: number){
    this.selectedRating = index + 1;
  }

  handleMouseLeave(){
    if (this.clickedRating !== 0){
      this.selectedRating = this.clickedRating;
    }
    else {
      this.selectedRating = 0;
    }
  }

  handleClick(index: number){
    this.selectedRating = index + 1;
    this.clickedRating = this.selectedRating;
    this.rated.emit(this.selectedRating);
  }
}
