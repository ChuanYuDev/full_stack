import {Component, inject, OnInit} from '@angular/core';
import {FormBuilder, ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatButtonModule} from "@angular/material/button";
import {GenreDTO} from "../../genres/genres.models";
import {MoviesListComponent} from "../movies-list/movies-list.component";
import {MoviesSearchDTO} from "./movies-search.model";

@Component({
    selector: 'app-movies-search',
    imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatCheckboxModule, MatButtonModule, MoviesListComponent],
    templateUrl: './movies-search.component.html',
    styleUrl: './movies-search.component.css'
})
export class MoviesSearchComponent implements OnInit{
    private formBuilder = inject(FormBuilder);

    form = this.formBuilder.group({
        title: "",
        genreId: 0,
        upcomingReleases: false,
        inTheaters: false
    });

    genres: GenreDTO[] = [
        {id: 1, name: "Comedy"},
        {id: 2, name: "Action"},
        {id: 3, name: "Drama"},
    ];

    moviesOriginal = [
        {
            title: 'Inside Out 2',
            releaseDate: new Date(),
            price: 1400.99,
            poster: 'https://upload.wikimedia.org/wikipedia/en/f/f7/Inside_Out_2_poster.jpg?20240514232832',
            genres: [1, 2, 3],
            upcomingRelease: true,
            inTheaters: false
        },
        {
            title: 'Moana 2',
            releaseDate: new Date('2016-05-03'),
            price: 300.99,
            poster: 'https://upload.wikimedia.org/wikipedia/en/7/73/Moana_2_poster.jpg',
            genres: [3],
            upcomingRelease: false,
            inTheaters: true
        },
        {
            title: 'Bad Boys: Ride or Die',
            releaseDate: new Date('2016-05-03'),
            price: 300.99,
            poster: 'https://upload.wikimedia.org/wikipedia/en/8/8b/Bad_Boys_Ride_or_Die_%282024%29_poster.jpg',
            genres: [1, 3],
            upcomingRelease: true,
            inTheaters: false
        },
        {
            title: 'Deadpool & Wolverine',
            releaseDate: new Date('2016-05-03'),
            price: 300.99,
            poster: 'https://upload.wikimedia.org/wikipedia/en/thumb/4/4c/Deadpool_%26_Wolverine_poster.jpg/220px-Deadpool_%26_Wolverine_poster.jpg',
            genres: [3],
            upcomingRelease: false,
            inTheaters: false

        },
        {
            title: 'Oppenheimer',
            releaseDate: new Date('2016-05-03'),
            price: 300.99,
            poster: 'https://upload.wikimedia.org/wikipedia/en/thumb/4/4a/Oppenheimer_%28film%29.jpg/220px-Oppenheimer_%28film%29.jpg',
            genres: [2],
            upcomingRelease: false,
            inTheaters: true
        },
        {
            title: 'The Flash',
            releaseDate: new Date('2016-05-03'),
            price: 300.99,
            poster: 'https://upload.wikimedia.org/wikipedia/en/thumb/e/ed/The_Flash_%28film%29_poster.jpg/220px-The_Flash_%28film%29_poster.jpg',
            genres: [1, 2, 3],
            upcomingRelease: true,
            inTheaters: false
        }];

    movies = this.moviesOriginal;

    ngOnInit(): void {
        this.form.valueChanges.subscribe(value => {
            this.movies = this.moviesOriginal;
            this.filterMovies(value as MoviesSearchDTO);
        });
    }

    filterMovies(value: MoviesSearchDTO) {
        if (value.title) {
            this.movies = this.movies.filter(movie => movie.title.indexOf(value.title) !== -1);
        }

        if (value.genreId) {
            this.movies = this.movies.filter(movie => movie.genres.indexOf(value.genreId) !== -1);
        }

        if (value.upcomingReleases) {
            this.movies = this.movies.filter(movie => movie.upcomingRelease);
        }

        if (value.inTheaters) {
            this.movies = this.movies.filter(movie => movie.inTheaters);
        }
    }

    clear() {
        this.form.patchValue({
            title: "",
            genreId: 0,
            upcomingReleases: false,
            inTheaters: false
        });
    }
}
