import {inject, Injectable} from '@angular/core';
import {GenreCreationDto, GenreDto} from "./genres.models";
import {HttpClient, HttpParams, HttpResponse} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {PaginationDTO} from "../shared/models/pagination.model";
import {buildQueryParams} from "../shared/functions/buildQueryParams";
import {ICRUDService} from "../shared/interfaces/ICRUDService";

@Injectable({
    providedIn: 'root'
})
export class GenresService implements ICRUDService<GenreDto, GenreCreationDto>{
    
    private http = inject(HttpClient);
    private baseUrl = environment.apiUrl + "/genres";
    
    constructor() { }
    
    getAll(): Observable<GenreDto[]> {
        return this.http.get<GenreDto[]>(`${this.baseUrl}/all`);
    }
    
    getPaginated(pagination: PaginationDTO): Observable<HttpResponse<GenreDto[]>> {
        const queryParams = buildQueryParams(pagination);
        return this.http.get<GenreDto[]>(this.baseUrl, {
            params: queryParams,
            observe: "response"
        });
    }
    
    getById(id: number): Observable<GenreDto>{
        return this.http.get<GenreDto>(`${this.baseUrl}/${id}`);
    }
    
    create(genre: GenreCreationDto): Observable<Object> {
        return this.http.post(this.baseUrl, genre);
    }
    
    update(id: number, genre: GenreCreationDto): Observable<Object> {
        return this.http.put(`${this.baseUrl}/${id}`, genre);
    }
    
    delete(id: number): Observable<Object> {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
}
