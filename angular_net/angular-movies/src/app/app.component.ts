import { CurrencyPipe, DatePipe, NgFor, UpperCasePipe } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  // From Angular 19 onwards, `standalone: true` is not necessary in component decorator, since components are standalone by default

  // Because of standalone application, we need to list the dependencies of the object in the object itself
  imports: [DatePipe, UpperCasePipe, CurrencyPipe, NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  // title = 'this is a custom title';

  // duplicateNumber(value: number): number {
  //   return value * 2;
  // }

  movies = [
    {
      title: 'Spider-Man',
      releaseDate: new Date(),
      price: 1400.99 
    },
    {
      title: 'Moana',
      releaseDate: new Date("2016-05-03"),
      price: 300.99 
    }
  ]
}
