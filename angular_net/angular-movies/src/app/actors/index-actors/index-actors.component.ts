import {Component} from '@angular/core';
import {ActorsService} from "../actors.service";
import {IndexEntitiesComponent} from "../../shared/components/index-entities/index-entities.component";
import {CRUD_SERVICE_TOKEN} from "../../shared/providers/providers";

@Component({
    selector: 'app-index-actors',
    imports: [IndexEntitiesComponent],
    templateUrl: './index-actors.component.html',
    styleUrl: './index-actors.component.css',
    providers: [{provide: CRUD_SERVICE_TOKEN, useClass: ActorsService}]
})
export class IndexActorsComponent {
}
