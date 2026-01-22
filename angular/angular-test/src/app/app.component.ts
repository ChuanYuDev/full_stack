import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {PatchChangeValuesComponent} from "./test/patch-change-values/patch-change-values.component";
import {PipeComponent} from "./test/pipe/pipe.component";
import {CreateCardComponent} from "./test/content-projection/create-card/create-card.component";

@Component({
    selector: 'app-root',
    imports: [RouterOutlet, PatchChangeValuesComponent, PipeComponent, CreateCardComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent {
    title = 'angular-test';
}
