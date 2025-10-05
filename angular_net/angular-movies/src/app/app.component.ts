import { Component } from '@angular/core';
import {MenuComponent} from "./shared/components/menu/menu.component";
import {MatButtonModule} from "@angular/material/button";
import {RouterOutlet} from "@angular/router";

@Component({
    selector: 'app-root',
    imports: [MenuComponent, MatButtonModule, RouterOutlet],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent {
}
