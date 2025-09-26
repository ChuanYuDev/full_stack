import {Component, Input} from '@angular/core';
import {CurrencyPipe, DatePipe, UpperCasePipe} from "@angular/common";

@Component({
  selector: 'app-generic-list',
    imports: [
        CurrencyPipe,
        DatePipe,
        UpperCasePipe
    ],
  templateUrl: './generic-list.component.html',
  styleUrl: './generic-list.component.css'
})
export class GenericListComponent {
    @Input({required: true})
    elements: any;
}
