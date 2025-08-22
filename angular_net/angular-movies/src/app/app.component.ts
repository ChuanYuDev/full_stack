import { CurrencyPipe, DatePipe, NgFor, NgIf, NgOptimizedImage, UpperCasePipe } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MoviesListComponent } from "./movies/movies-list/movies-list.component";

@Component({
  selector: 'app-root',
  // From Angular 19 onwards, `standalone: true` is not necessary in component decorator, since components are standalone by default

  // Because of standalone application, we need to list the dependencies of the object in the object itself
  // imports: [NgFor, NgOptimizedImage, NgIf],
  imports: [MoviesListComponent],
  
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  // title = 'this is a custom title';

  // duplicateNumber(value: number): number {
  //   return value * 2;
  // }

}
