import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActorCreationDTO, ActorDTO } from '../actors.models';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker'
import moment from 'moment';

@Component({
    selector: 'app-actors-form',
    imports: [ReactiveFormsModule, MatButtonModule, RouterLink, MatFormFieldModule, MatInputModule, MatDatepickerModule],
    templateUrl: './actors-form.component.html',
    styleUrl: './actors-form.component.css'
})
export class ActorsFormComponent implements OnInit{
    private formBuilder = inject(FormBuilder);

    form = this.formBuilder.group({
        name: ['', {validators: [Validators.required]}],
        dateOfBirth: new FormControl<Date | null>(null)
    });

    @Input()
    model?: ActorDTO;

    @Output()
    postForm = new EventEmitter<ActorCreationDTO>();

    ngOnInit(): void {
        if (this.model !== undefined){
            this.form.patchValue(this.model);
        }
    }

    getErrorMessagesForName(): string {
        let field = this.form.controls.name;

        if (field.hasError('required')) {
            return "The name field is required";
        }

        return "";
    }

    saveChanges() {
        const actor = this.form.value as ActorCreationDTO;
        
        // For Date of birth field, what we're storing is a `moment.js` object
        //      We can transform it into a normal date in JavaScript
        actor.dateOfBirth = moment(actor.dateOfBirth).toDate();

        this.postForm.emit(actor);
    }

}
