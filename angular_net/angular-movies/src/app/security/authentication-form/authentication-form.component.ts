import {Component, EventEmitter, inject, Input, Output} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {UserCredentialsDto} from "../security.models";
import {MatFormField, MatInput, MatLabel} from "@angular/material/input";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatButtonModule} from "@angular/material/button";
import {RouterLink} from "@angular/router";

@Component({
    selector: 'app-authentication-form',
    imports: [ReactiveFormsModule, MatFormFieldModule, MatInput, MatButtonModule, RouterLink],
    templateUrl: './authentication-form.component.html',
    styleUrl: './authentication-form.component.css'
})
export class AuthenticationFormComponent {
    private formBuilder = inject(FormBuilder);
    
    form = this.formBuilder.group({
        email: ["", {validators: [Validators.required, Validators.email]}],
        password: ["", {validators: [Validators.required]}]
    });
    
    @Output()
    postForm = new EventEmitter<UserCredentialsDto>();
    
    getErrorMessagesForEmail() {
        const field = this.form.controls.email;
        
        if (field.hasError("required")) {
            return "The email field is required";
        }
        
        if (field.hasError("email")) {
            return "The email field is not valid";
        } 
        
        return ""
    }
    
    getErrorMessagesForPassword() {
        const field = this.form.controls.password;
        
        if (field.hasError("required")) {
            return "The password field is required";
        } 
        
        return "";
    }
    
    saveChanges() {
        const userCredentialsDto = this.form.value as UserCredentialsDto;
        this.postForm.emit(userCredentialsDto);
    }
}
