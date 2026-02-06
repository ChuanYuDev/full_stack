import {inject, Injectable} from '@angular/core';
import {GenreCreationDTO, GenreDTO} from "./genres.models";
import {HttpClient, HttpResponse} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {PaginationDTO} from "../shared/models/pagination.model";
import {buildQueryParams} from "../shared/functions/buildQueryParams";

@Injectable({
    providedIn: 'root'
})
export class GenresService {
    constructor() { }
    
    private http = inject(HttpClient);
    private baseURL = environment.apiURL + "/genres";
    
    getAll(): Observable<GenreDTO[]> {
        return this.http.get<GenreDTO[]>(this.baseURL);
    }
    
    getPaginated(pagination: PaginationDTO): Observable<HttpResponse<GenreDTO[]>> {
        const queryParams = buildQueryParams(pagination);
        return this.http.get<GenreDTO[]>(this.baseURL, {
            params: queryParams,
            observe: "response"
        });
    }
    
    create(genre: GenreCreationDTO) {
        return this.http.post(this.baseURL, genre);
    }
}
