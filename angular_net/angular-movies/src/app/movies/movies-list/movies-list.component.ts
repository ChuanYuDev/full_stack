import { CurrencyPipe, DatePipe, UpperCasePipe } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-movies-list',
  imports: [DatePipe, UpperCasePipe, CurrencyPipe],
  templateUrl: './movies-list.component.html',
  styleUrl: './movies-list.component.css'
})
export class MoviesListComponent {
  // Empty array is true in condition
  // movies: any[] = [];

  // Use Input decorator to indicate movies property is input from parent component
  @Input({required: true})
  movies?: any[];
}
