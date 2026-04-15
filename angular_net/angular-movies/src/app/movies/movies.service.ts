import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {LandingDto, MovieCreationDto, MovieDetailsDto, MoviePostGetDto, MoviePutGetDto} from "./movies.models";
import {environment} from "../../environments/environment";

@Injectable({
    providedIn: 'root'
})
export class MoviesService {
    private http = inject(HttpClient);
    private baseUrl = environment.apiUrl + "/movies";
    constructor() { }
    
    getLanding(): Observable<LandingDto> {
        return this.http.get<LandingDto>(`${this.baseUrl}/landing`)
    }
    
    // filter()
    
    getById(id: number): Observable<MovieDetailsDto> {
        return this.http.get<MovieDetailsDto>(`${this.baseUrl}/${id}`);
    }
    
    postGet(): Observable<MoviePostGetDto> {
        return this.http.get<MoviePostGetDto>(`${this.baseUrl}/post-get`);
    }
    
    putGet(id: number): Observable<MoviePutGetDto> {
        return this.http.get<MoviePutGetDto>(`${this.baseUrl}/put-get/${id}`);
    }
    
    post(movie: MovieCreationDto) {
        const formData = this.buildFormData(movie);
        return this.http.post(this.baseUrl, formData);
    }
    
    update(id: number, movieCreationDto: MovieCreationDto) {
        const formData = this.buildFormData(movieCreationDto);
        return this.http.put(`${this.baseUrl}/${id}`, formData);
    }
    
    delete(id: number) {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
    
    private buildFormData(movie: MovieCreationDto): FormData {
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
