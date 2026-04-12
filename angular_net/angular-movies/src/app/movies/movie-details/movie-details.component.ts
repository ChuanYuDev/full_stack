import {Component, inject, Input, numberAttribute, OnInit} from '@angular/core';
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";
import {MoviesService} from "../movies.service";
import {MovieDetailsDto} from "../movies.models";
import {LoadingComponent} from "../../shared/components/loading/loading.component";
import {MatChipsModule} from "@angular/material/chips";
import {RouterLink} from "@angular/router";

@Component({
    selector: 'app-movie-details',
    imports: [LoadingComponent, MatChipsModule, RouterLink],
    templateUrl: './movie-details.component.html',
    styleUrl: './movie-details.component.css'
})
export class MovieDetailsComponent implements OnInit{
    
    moviesService = inject(MoviesService);
    sanitizer = inject(DomSanitizer);
    
    movie?: MovieDetailsDto;
    trailerUrl?: SafeResourceUrl | string;
    
    @Input({transform: numberAttribute})
    id: number = 0;
    
    ngOnInit() {
        this.moviesService.getById(this.id).subscribe(movieDetailsDto => {
           this.movie = movieDetailsDto; 
           
           if (this.movie.releaseDate) {
               this.movie.releaseDate = new Date(this.movie.releaseDate);
           }
           
           if (this.movie.trailer) {
               this.trailerUrl = this.transformYoutubeUrlToEmbed(this.movie.trailer);
           } 
           
           console.log(this.movie);
           console.log(typeof this.movie.releaseDate);
        });
    }
    
    transformYoutubeUrlToEmbed(url: string): SafeResourceUrl | string {
        if (!url) {
            return "";
        }

        //https://www.youtube.com/watch?v=dQw4w9WgXcQ&pp=ygUjcmljayBhc3RsZXkgbmV2ZXIgZ29ubmEgZ212ZSB5b3UgdXA%3D
        let videoId = url.split("v=")[1];
        const ampersandPosition = url.indexOf("&");
        
        if (ampersandPosition !== -1) {
            videoId = videoId.substring(0, ampersandPosition);
        } 
        
        return this.sanitizer.bypassSecurityTrustResourceUrl(`https://www.youtube.com/embed/${videoId}`);
    }
}
