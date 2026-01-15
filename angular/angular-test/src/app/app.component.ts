import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {PatchChangeValuesComponent} from "./test/patch-change-values/patch-change-values.component";
import {PipeComponent} from "./test/pipe/pipe.component";

@Component({
    selector: 'app-root',
    imports: [RouterOutlet, PatchChangeValuesComponent, PipeComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent {
    title = 'angular-test';
}
