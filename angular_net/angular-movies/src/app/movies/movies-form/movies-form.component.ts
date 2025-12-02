import {Component, EventEmitter, inject, Input, OnInit, Output} from '@angular/core';
import {MovieCreationDTO, MovieDTO} from "../movies.models";
import {FormBuilder, FormControl, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatButtonModule} from "@angular/material/button";
import {RouterLink} from "@angular/router";
import moment from "moment";
import {InputImgComponent} from "../../shared/components/input-img/input-img.component";
import {MultipleSelectorDTO} from "../../shared/components/multiple-selector/multiple-selector.model";
import {MultipleSelectorComponent} from "../../shared/components/multiple-selector/multiple-selector.component";
import {ActorsAutocompleteComponent} from "../../actors/actors-autocomplete/actors-autocomplete.component";

@Component({
    selector: 'app-movies-form',
    imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, MatButtonModule, RouterLink, InputImgComponent, MultipleSelectorComponent, ActorsAutocompleteComponent],
    templateUrl: './movies-form.component.html',
    styleUrl: './movies-form.component.css'
})
export class MoviesFormComponent implements OnInit{
    private formBuilder = inject(FormBuilder);

    @Input()
    model?: MovieDTO;

    @Input({required: true})
    selectedGenres: MultipleSelectorDTO[] = [];

    @Input({required: true})
    nonSelectedGenres: MultipleSelectorDTO[] = [];

    @Input({required: true})
    selectedTheaters: MultipleSelectorDTO[] = [];

    @Input({required: true})
    nonSelectedTheaters: MultipleSelectorDTO[] = [];

    @Output()
    postForm = new EventEmitter<MovieCreationDTO>();

    form = this.formBuilder.group({
        title: ["", {validators: [Validators.required]}],
        releaseDate: new FormControl<Date | null>(null),
        trailer: "",
        poster: new FormControl<File | string | null>(null),
    });

    ngOnInit() {
        if (this.model){
            this.form.patchValue(this.model);
        }
    }

    getErrorMessagesForTitle(): string {
        const field = this.form.controls.title;

        if (field.hasError("required")) {
            return "The title field is required";
        }

        return "";
    }

    handleSelectedFile(file: File): void {
        this.form.controls.poster.setValue(file);
    }

    saveChanges() {
        const movie = this.form.value as MovieCreationDTO;

        if (movie.releaseDate) {
            movie.releaseDate = moment(movie.releaseDate).toDate();
        }

        if (typeof movie.poster === "string") {
            movie.poster = undefined;
        }

        movie.genresIds = this.selectedGenres.map(val => val.key);
        movie.theatersIds = this.selectedTheaters.map(val => val.key);

        this.postForm.emit(movie);
    }
}
