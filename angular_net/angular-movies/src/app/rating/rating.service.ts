import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

@Injectable({
    providedIn: 'root'
})
export class RatingService {
    
    private http = inject(HttpClient);
    private baseUrl = environment.apiUrl + "/ratings";

    constructor() { }
    
    rate(movieId: number,  rate: number) {
        return this.http.put(this.baseUrl, {movieId, rate});
    }
}
