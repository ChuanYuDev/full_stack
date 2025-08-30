import { Component } from '@angular/core';
import { MoviesListComponent } from "../movies/movies-list/movies-list.component";
import { RatingComponent } from "../shared/components/rating/rating.component";

@Component({
  selector: 'app-landing-page',
  imports: [MoviesListComponent, RatingComponent],
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.css'
})
export class LandingPageComponent {

    inTheatersMovies: any;
    upcomingReleasesMovies: any;

    // Simulate the real situation in which it takes sometime to load the movies list from database
    constructor(){
        setTimeout(() => {
            this.inTheatersMovies = [
                {
                title: 'Inside Out 2',
                releaseDate: new Date(),
                price: 1400.99,
                poster: 'https://upload.wikimedia.org/wikipedia/en/f/f7/Inside_Out_2_poster.jpg?20240514232832'
                },
                {
                title: 'Moana 2',
                releaseDate: new Date('2016-05-03'),
                price: 300.99,
                poster: 'https://upload.wikimedia.org/wikipedia/en/7/73/Moana_2_poster.jpg'
                }
            ]

            this.upcomingReleasesMovies = [
                {
                title: 'Bad Boys: Ride or Die',
                releaseDate: new Date('2016-05-03'),
                price: 300.99,
                poster: 'https://upload.wikimedia.org/wikipedia/en/8/8b/Bad_Boys_Ride_or_Die_%282024%29_poster.jpg'
                },
                {
                title: 'Deadpool & Wolverine',
                releaseDate: new Date('2016-05-03'),
                price: 300.99,
                poster: 'https://upload.wikimedia.org/wikipedia/en/thumb/4/4c/Deadpool_%26_Wolverine_poster.jpg/220px-Deadpool_%26_Wolverine_poster.jpg'
                },
                {
                title: 'Oppenheimer',
                releaseDate: new Date('2016-05-03'),
                price: 300.99,
                poster: 'https://upload.wikimedia.org/wikipedia/en/thumb/4/4a/Oppenheimer_%28film%29.jpg/220px-Oppenheimer_%28film%29.jpg'
                },
                {
                title: 'The Flash',
                releaseDate: new Date('2016-05-03'),
                price: 300.99,
                poster: 'https://upload.wikimedia.org/wikipedia/en/thumb/e/ed/The_Flash_%28film%29_poster.jpg/220px-The_Flash_%28film%29_poster.jpg'
                }
            ];

        }, 100);
    }

    processRating(rate: number) {
        // Template literals are literals delimited with backtick (`) characters
        //    Allowing for multi-line strings, string interpolation with embedded expressions, and special constructs called tagged templates
        alert(`You rated the movie ${rate}`);
    }
}
