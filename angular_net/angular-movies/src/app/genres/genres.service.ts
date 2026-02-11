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
    
    getById(id: number): Observable<GenreDTO>{
        return this.http.get<GenreDTO>(`${this.baseURL}/${id}`);
    }
    
    create(genre: GenreCreationDTO): Observable<Object> {
        return this.http.post(this.baseURL, genre);
    }
    
    update(id: number, genre: GenreCreationDTO): Observable<Object> {
        return this.http.put(`${this.baseURL}/${id}`, genre);
    }
    
    delete(id: number): Observable<Object> {
        return this.http.delete(`${this.baseURL}/${id}`);
    }
}
