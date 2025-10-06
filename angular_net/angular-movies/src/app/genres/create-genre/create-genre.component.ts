import {Component, inject} from '@angular/core';
import {Router} from "@angular/router";
import {MatButtonModule} from "@angular/material/button";

@Component({
    selector: 'app-create-genre',
    imports: [MatButtonModule],
    templateUrl: './create-genre.component.html',
    styleUrl: './create-genre.component.css'
})
export class CreateGenreComponent {
    
    router = inject(Router);
    saveChanges() {
        this.router.navigate(["/genres"]);
    }
}
