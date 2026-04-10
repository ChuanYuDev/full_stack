import {Component, inject} from '@angular/core';
import {MoviesListComponent} from "../movies/movies-list/movies-list.component";
import {MoviesService} from "../movies/movies.service";
import {MovieDto} from "../movies/movies.models";

@Component({
    selector: 'app-landing-page',
    imports: [MoviesListComponent],
    templateUrl: './landing-page.component.html',
    styleUrl: './landing-page.component.css'
})
export class LandingPageComponent {
    upcomingReleasesMovies?: MovieDto[];
    inTheatersMovies?: MovieDto[];
    
    moviesService = inject(MoviesService);
    
    constructor() {
        this.loadMovies();
    }
    
    loadMovies() {
        this.moviesService.getLanding().subscribe(landingDto => {
            this.upcomingReleasesMovies = landingDto.upcomingReleases;
            this.inTheatersMovies = landingDto.inTheaters;
        });
    }
    
    handleDelete() {
        this.loadMovies();
    }
}
