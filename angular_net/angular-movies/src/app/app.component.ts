import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'this is a custom title';

  movie = {
    title: 'Spider-Man',
    releaseDate: new Date()
  }

  duplicateNumber(value: number): number {
    return value * 2;
  }
}
