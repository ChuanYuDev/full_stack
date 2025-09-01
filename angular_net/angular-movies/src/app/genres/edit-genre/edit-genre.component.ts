import { Component, Input, numberAttribute } from '@angular/core';
import { GenreCreationDTO, GenreDTO } from '../genres.models';
import { GenresFormComponent } from "../genres-form/genres-form.component";

@Component({
  selector: 'app-edit-genre',
  imports: [GenresFormComponent],
  templateUrl: './edit-genre.component.html',
  styleUrl: './edit-genre.component.css'
})
export class EditGenreComponent {
    @Input({transform: numberAttribute})
    id!: number;
    
    // We want to pass it down to our genres-form component so that on the name control Action appears
    model: GenreDTO = {id: 1, name: 'Action'};

    // Simply reuse CreateDTO for updates because most of the time they have the same properties
    //      If we have the need to use another DTO, we can create DTO such as GenreUpdateDTO
    saveChanges(genre: GenreCreationDTO){
        console.log('Editing the genre', genre);
    }
}
