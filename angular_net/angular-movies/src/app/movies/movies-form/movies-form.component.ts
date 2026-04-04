import {Component, EventEmitter, inject, Input, OnInit, Output} from '@angular/core';
import {MovieCreationDto, MovieDto} from "../movies.models";
import {FormBuilder, FormControl, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatButtonModule} from "@angular/material/button";
import {RouterLink} from "@angular/router";
import moment from "moment";
import {InputImgComponent} from "../../shared/components/input-img/input-img.component";
import {MultipleSelectorDto} from "../../shared/components/multiple-selector/multiple-selector.model";
import {MultipleSelectorComponent} from "../../shared/components/multiple-selector/multiple-selector.component";
import {ActorsAutocompleteComponent} from "../../actors/actors-autocomplete/actors-autocomplete.component";
import {ActorAutoCompleteDto} from "../../actors/actors.models";

@Component({
    selector: 'app-movies-form',
    imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, MatButtonModule, RouterLink, InputImgComponent, MultipleSelectorComponent, ActorsAutocompleteComponent],
    templateUrl: './movies-form.component.html',
    styleUrl: './movies-form.component.css'
})
export class MoviesFormComponent implements OnInit{
    @Input()
    model?: MovieDto;

    @Input({required: true})
    selectedGenres: MultipleSelectorDto[] = [];

    @Input({required: true})
    nonSelectedGenres: MultipleSelectorDto[] = [];

    @Input({required: true})
    selectedTheaters: MultipleSelectorDto[] = [];

    @Input({required: true})
    nonSelectedTheaters: MultipleSelectorDto[] = [];

    @Input({required: true})
    selectedActors: ActorAutoCompleteDto[] = [];

    @Output()
    postForm = new EventEmitter<MovieCreationDto>();

    private formBuilder = inject(FormBuilder);

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
        const movie = this.form.value as MovieCreationDto;

        if (movie.releaseDate) {
            movie.releaseDate = moment(movie.releaseDate).toDate();
        }

        if (typeof movie.poster === "string") {
            movie.poster = undefined;
        }

        movie.genresIds = this.selectedGenres.map(val => val.key);
        movie.theatersIds = this.selectedTheaters.map(val => val.key);
        movie.actors = this.selectedActors;

        this.postForm.emit(movie);
    }
}
