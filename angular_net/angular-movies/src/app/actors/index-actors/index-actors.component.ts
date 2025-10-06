import { Component } from '@angular/core';
import {RouterLink} from "@angular/router";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";

@Component({
  selector: 'app-index-actors',
    imports: [MatButtonModule, MatIconModule,RouterLink],
  templateUrl: './index-actors.component.html',
  styleUrl: './index-actors.component.css'
})
export class IndexActorsComponent {

}
