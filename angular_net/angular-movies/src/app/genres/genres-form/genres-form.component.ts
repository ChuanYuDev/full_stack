import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RouterLink } from '@angular/router';
import { firstLetterShouldBeUppercase } from '../../shared/functions/validations';
import { GenreCreationDTO, GenreDTO } from '../genres.models';

@Component({
    selector: 'app-genres-form',
    imports: [MatButtonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, RouterLink],
    templateUrl: './genres-form.component.html',
    styleUrl: './genres-form.component.css'
})
export class GenresFormComponent implements OnInit{
    private formBuilder = inject(FormBuilder);

    // Group: We will have a group of controls
    form = this.formBuilder.group({
        // '': Represents the initial value of this control
        //
        // Validators.required
        //      My name control must have a value so that the form is considered valid
        name: ['', {validators: [Validators.required, firstLetterShouldBeUppercase()]}]
    });

    // model is GenreDTO | undefined
    @Input()
    model?: GenreDTO;

    @Output()
    postForm = new EventEmitter<GenreCreationDTO>();

    ngOnInit(): void {
        if (this.model !== undefined){
            this.form.patchValue(this.model);
        }
    }

    getErrorMessagesForName(): string {
        // field is not the content of name, this is a representation of the name field
        let field = this.form.controls.name;

        if (field.hasError('required')){
            return "The name field is required";

            // Long error message
            // return "The name field is required The name field is required The name field is required The name field is required The name field is required The name field is required The name field is required The name field is required The name field is required The name field is required The name field is required The name field is required";
        }

        if (field.hasError('firstLetterShouldBeUppercase')) {
            return field.getError('firstLetterShouldBeUppercase').message;
        }

        return "";
    }

    saveChanges() {
        const genre = this.form.value as GenreCreationDTO;
        this.postForm.emit(genre);
    }
}
