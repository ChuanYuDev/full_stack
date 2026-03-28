import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {MovieCreationDTO, MoviePostGetDTO} from "./movies.models";
import {environment} from "../../environments/environment";

@Injectable({
    providedIn: 'root'
})
export class MoviesService {
    private http = inject(HttpClient);
    private baseURL = environment.apiURL + "/movies";
    constructor() { }
    
    postGet(): Observable<MoviePostGetDTO> {
        return this.http.get<MoviePostGetDTO>(`${this.baseURL}/post-get`);
    }
    
    post(movie: MovieCreationDTO) {
        const formData = this.buildFormData(movie);
        return this.http.post(this.baseURL, formData);
    }
    
    private buildFormData(movie: MovieCreationDTO): FormData {
        const formData = new FormData();
        
        formData.append("title", movie.title);
        
        if (movie.releaseDate) {
            formData.append("releaseDate", movie.releaseDate.toISOString().split("T")[0]);
        }
        
        if (movie.trailer) {
            formData.append("trailer", movie.trailer);
        } 
        
        if (movie.poster) {
            formData.append("poster", movie.poster);
        } 
        
        if (movie.genresIds) {
            formData.append("genresIds", JSON.stringify(movie.genresIds));
        }
        
        if (movie.theatersIds) {
            formData.append("theatersIds", JSON.stringify(movie.theatersIds));
        }
        
        if (movie.actors) {
            formData.append("actors", JSON.stringify(movie.actors));
        }
            
        return formData;
    }
}
