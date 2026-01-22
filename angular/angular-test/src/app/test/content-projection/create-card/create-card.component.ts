import { Component } from '@angular/core';
import {CustomCardComponent} from "../custom-card/custom-card.component";

@Component({
  selector: 'app-create-card',
    imports: [
        CustomCardComponent
    ],
  templateUrl: './create-card.component.html',
  styleUrl: './create-card.component.css'
})
export class CreateCardComponent {

}
