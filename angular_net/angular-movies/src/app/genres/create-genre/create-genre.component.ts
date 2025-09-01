import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterLink } from '@angular/router';
import { firstLetterShouldBeUppercase } from '../../shared/functions/validations';
import { GenreCreationDTO } from '../genres.models';
import { GenresFormComponent } from "../genres-form/genres-form.component";

@Component({
    selector: 'app-create-genre',
    imports: [MatButtonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, GenresFormComponent],
    templateUrl: './create-genre.component.html',
    styleUrl: './create-genre.component.css'
})
export class CreateGenreComponent {
    // Inject is a special function that will allow us to get a hold of a service that's already configured
    router = inject(Router);

    saveChanges(genre: GenreCreationDTO) {
        // .. Save changes
        // console.log(this.form.value);
        console.log(genre);

        // Navigate using JavaScript, we have to use a service
        //      Service is a piece of functionality that we can reuse throughout the whole application
        //      We don't have to configure them, but they come pre-configured 
        this.router.navigate(['/genres']);
    }
}
