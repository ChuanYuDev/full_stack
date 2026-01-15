import { Component } from '@angular/core';
import {CurrencyPipe, DatePipe} from "@angular/common";

@Component({
    selector: 'app-pipe',
    imports: [DatePipe, CurrencyPipe],
    templateUrl: './pipe.component.html',
    styleUrl: './pipe.component.css'
})
export class PipeComponent {
    movie = {
        title: "Spider-Man",   
        releaseDate: new Date(),
        price: 1400.99,
    };
}
