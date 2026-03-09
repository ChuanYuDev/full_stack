import {Component} from '@angular/core';
import {GenresFormComponent} from "../genres-form/genres-form.component";
import {GenresService} from "../genres.service";
import {CreateEntityComponent} from "../../shared/components/create-entity/create-entity.component";
import {CRUD_SERVICE_TOKEN} from "../../shared/providers/providers";

@Component({
    selector: 'app-create-genre',
    imports: [CreateEntityComponent],
    templateUrl: './create-genre.component.html',
    styleUrl: './create-genre.component.css',
    providers: [{provide: CRUD_SERVICE_TOKEN, useClass: GenresService}]
})
export class CreateGenreComponent {
    readonly genresForm = GenresFormComponent;
}