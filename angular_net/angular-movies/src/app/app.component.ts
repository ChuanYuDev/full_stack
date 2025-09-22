import { Component } from '@angular/core';
import {CurrencyPipe, DatePipe, UpperCasePipe} from "@angular/common";

@Component({
    selector: 'app-root',
    imports: [DatePipe, UpperCasePipe, CurrencyPipe],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent {
    title = 'this is a custom title';

    duplicateNumber(value: number): number {
        return value * 2;
    }
    
    movie = {
        title: "Spider-Man",
        releaseDate: new Date(),
        price: 1400.99,
    };

}
