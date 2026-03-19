import { Component } from '@angular/core';
import {IndexEntitiesComponent} from "../../shared/components/index-entities/index-entities.component";
import {CRUD_SERVICE_TOKEN} from "../../shared/providers/providers";
import {TheatersService} from "../theaters.service";

@Component({
    selector: 'app-index-theaters',
    imports: [IndexEntitiesComponent],
    templateUrl: './index-theaters.component.html',
    styleUrl: './index-theaters.component.css',
    providers: [{provide: CRUD_SERVICE_TOKEN, useClass: TheatersService}]
})
export class IndexTheatersComponent {

}
