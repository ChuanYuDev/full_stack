import {Component, EventEmitter, inject, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {TheaterCreationDTO, TheaterDTO} from "../theaters.models";
import {MatButtonModule} from "@angular/material/button";
import {MatFormFieldModule} from "@angular/material/form-field";
import {RouterLink} from "@angular/router";
import {MatInputModule} from "@angular/material/input";

@Component({
    selector: 'app-theaters-form',
    imports: [ReactiveFormsModule, MatButtonModule, MatFormFieldModule, MatInputModule, RouterLink],
    templateUrl: './theaters-form.component.html',
    styleUrl: './theaters-form.component.css'
})
export class TheatersFormComponent implements OnInit{
    private formBuilder = inject(FormBuilder);
    
    form = this.formBuilder.group({
        name: ["", {validators: [Validators.required]}],
    });
    
    @Input()
    model?: TheaterDTO;
    
    @Output()
    postForm = new EventEmitter<TheaterCreationDTO>();
    
    ngOnInit() {
        if (this.model) {
            this.form.patchValue(this.model);
        }
    }
    
    getErrorMessagesForName(): string {
        const field = this.form.controls.name;
        
        if (field.hasError("required")) {
            return "The name field is required"; 
        }
        
        return "";
    }

    saveChanges() {
        const theater = this.form.value as TheaterCreationDTO;
        
        this.postForm.emit(theater);
    }
}