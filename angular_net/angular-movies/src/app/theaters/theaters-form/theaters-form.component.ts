import {Component, EventEmitter, inject, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, ReactiveFormsModule, Validators} from "@angular/forms";
import {TheaterCreationDTO, TheaterDTO} from "../theaters.models";
import {MatButtonModule} from "@angular/material/button";
import {MatFormFieldModule} from "@angular/material/form-field";
import {RouterLink} from "@angular/router";
import {MatInputModule} from "@angular/material/input";
import {MapComponent} from "../../shared/components/map/map.component";
import {Coordinate} from "../../shared/components/map/coordinate.model";

@Component({
    selector: 'app-theaters-form',
    imports: [ReactiveFormsModule, MatButtonModule, MatFormFieldModule, MatInputModule, RouterLink, MapComponent],
    templateUrl: './theaters-form.component.html',
    styleUrl: './theaters-form.component.css'
})
export class TheatersFormComponent implements OnInit{
    private formBuilder = inject(FormBuilder);
    
    form = this.formBuilder.group({
        name: ["", {validators: [Validators.required]}],
        coordinate: new FormControl<Coordinate | null>(null, {validators: [Validators.required]}),
    });
    
    @Input()
    model?: TheaterDTO;

    initialCoordinate: Coordinate[] = [];
    
    @Output()
    postForm = new EventEmitter<TheaterCreationDTO>();
    
    ngOnInit() {
        if (this.model) {
            this.form.patchValue(this.model);

            const coordinate: Coordinate = {latitude: this.model.latitude, longitude: this.model.longitude};
            this.handleSelectedCoordinate(coordinate);
            this.initialCoordinate.push(coordinate);
        }
    }
    
    getErrorMessagesForName(): string {
        const field = this.form.controls.name;
        
        if (field.hasError("required")) {
            return "The name field is required"; 
        }
        
        return "";
    }

    handleSelectedCoordinate(coordinate: Coordinate) {
        this.form.controls.coordinate.setValue(coordinate);
    }

    saveChanges() {
        const theater = this.form.value as TheaterCreationDTO;

        const coordinate = this.form.controls.coordinate.value;
        theater.latitude = coordinate?.latitude || 0;
        theater.longitude = coordinate?.longitude || 0;
        
        this.postForm.emit(theater);
    }
}