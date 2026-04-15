import {Component, inject, OnInit} from '@angular/core';
import {FormBuilder, ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatButtonModule} from "@angular/material/button";
import {GenreDto} from "../../genres/genres.models";
import {MoviesListComponent} from "../movies-list/movies-list.component";
import {ActivatedRoute} from "@angular/router";
import {Location} from "@angular/common";
import {MovieDto, MoviesSearchDto, MoviesSearchWithPaginationDto} from "../movies.models";
import {MoviesService} from "../movies.service";
import {GenresService} from "../../genres/genres.service";
import {PaginationDTO} from "../../shared/models/pagination.model";
import {MatPaginatorModule, PageEvent} from "@angular/material/paginator";

@Component({
    selector: 'app-movies-search',
    imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatCheckboxModule, MatButtonModule, MoviesListComponent, MatPaginatorModule],
    templateUrl: './movies-search.component.html',
    styleUrl: './movies-search.component.css'
})
export class MoviesSearchComponent implements OnInit{
    private formBuilder = inject(FormBuilder);
    activatedRoute = inject(ActivatedRoute);
    location = inject(Location);
    genresService = inject(GenresService);
    moviesService = inject(MoviesService);

    form = this.formBuilder.group({
        title: "",
        genreId: 0,
        upcomingReleases: false,
        inTheaters: false
    });

    genres: GenreDto[] = [];
    movies: MovieDto[] = [];
    pagination: PaginationDTO = {page: 1, recordsPerPage: 5};
    totalRecordsCount: number = 0;

    ngOnInit(): void {
        this.genresService.getAll().subscribe(genres => {
            this.genres = genres;
            
            this.form.valueChanges.subscribe(value => {
                this.writeParametersInURL(value as MoviesSearchDto);
                this.filterMovies(value as MoviesSearchDto);
            });

            this.readValuesFromURL();
        });
    }

    readValuesFromURL() {
        this.activatedRoute.queryParams.subscribe(params => {
            const obj: MoviesSearchDto = {};

            if (params["title"]) {
                obj.title = params["title"];
            }

            if (params["genreId"]) {
                obj.genreId = Number(params["genreId"]);
            }

            if (params["upcomingReleases"]) {
                obj.upcomingReleases = Boolean(params["upcomingReleases"]);
            }

            if (params["inTheaters"]) {
                obj.inTheaters = Boolean(params["inTheaters"]);
            }

            this.form.patchValue(obj);
        });
    }

    writeParametersInURL(value: MoviesSearchDto) {
        const queryStrings = [];

        if (value.title) {
            queryStrings.push(`title=${encodeURIComponent(value.title)}`);
        }

        if (value.genreId) {
            queryStrings.push(`genreId=${value.genreId}`);
        }

        if (value.upcomingReleases) {
            queryStrings.push(`upcomingReleases=${value.upcomingReleases}`);
        }

        if (value.inTheaters) {
            queryStrings.push(`inTheaters=${value.inTheaters}`);
        }

        this.location.replaceState("movies/search", queryStrings.join('&'));
    }

    filterMovies(moviesSearchDto: MoviesSearchDto) {
        const moviesSearchWithPaginationDto: MoviesSearchWithPaginationDto = Object.assign({}, moviesSearchDto, this.pagination);
        
        this.moviesService.filter(moviesSearchWithPaginationDto).subscribe(response => {
            this.movies = response.body as MovieDto[];
            const header = response.headers.get("total-records-count") as string;
            this.totalRecordsCount = parseInt(header, 10);
        });
    }

    clear() {
        this.form.patchValue({
            title: "",
            genreId: 0,
            upcomingReleases: false,
            inTheaters: false
        });
    }
    
    updatePagination(event: PageEvent) {
        this.pagination = {page: event.pageIndex + 1, recordsPerPage: event.pageSize};
        
        this.filterMovies(this.form.value as MoviesSearchDto);
    }
}
