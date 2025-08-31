import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-create-genre',
  imports: [MatButtonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, RouterLink],
  templateUrl: './create-genre.component.html',
  styleUrl: './create-genre.component.css'
})
export class CreateGenreComponent {
    // Inject is a special function that will allow us to get a hold of a service that's already configured
    router = inject(Router);

    private formBuilder = inject(FormBuilder);

    // Group: We will have a group of controls
    form = this.formBuilder.group({
        // '': Represents the initial value of this control
        name: ['']
    });

    saveChanges() {
        // .. Save changes
        console.log(this.form.value);

        // Navigate using JavaScript, we have to use a service
        //      Service is a piece of functionality that we can reuse throughout the whole application
        //      We don't have to configure them, but they come pre-configured 
        this.router.navigate(['/genres']);
    }
}
