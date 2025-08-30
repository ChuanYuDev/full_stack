import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-genre',
  imports: [MatButtonModule],
  templateUrl: './create-genre.component.html',
  styleUrl: './create-genre.component.css'
})
export class CreateGenreComponent {
    // Inject is a special function that will allow us to get a hold of a service that's already configured
    router = inject(Router);

    saveChange() {
        // .. Save changes

        // Navigate using JavaScript, we have to use a service
        //      Service is a piece of functionality that we can reuse throughout the whole application
        //      We don't have to configure them, but they come pre-configured 
        this.router.navigate(['/genres']);
    }
}
