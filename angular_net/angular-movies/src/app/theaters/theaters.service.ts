import {inject, Injectable} from '@angular/core';
import {ICRUDService} from "../shared/interfaces/ICRUDService";
import {TheaterCreationDto, TheaterDto} from "./theaters.models";
import {HttpClient, HttpResponse} from "@angular/common/http";
import { Observable } from "rxjs";
import { PaginationDTO } from "../shared/models/pagination.model";
import {environment} from "../../environments/environment";
import {buildQueryParams} from "../shared/functions/buildQueryParams";

@Injectable({
    providedIn: 'root'
})
export class TheatersService implements ICRUDService<TheaterDto, TheaterCreationDto> {

    private http = inject(HttpClient);
    private baseUrl = environment.apiUrl + "/theaters";

    constructor() {}
    
    getPaginated(pagination: PaginationDTO): Observable<HttpResponse<TheaterDto[]>> {
        const queryParams = buildQueryParams(pagination);
        return this.http.get<TheaterDto[]>(this.baseUrl, {
            params: queryParams,
            observe: "response"
        });
    }
    getById(id: number): Observable<TheaterDto> {
        return this.http.get<TheaterDto>(`${this.baseUrl}/${id}`);
    }
    create(entity: TheaterCreationDto): Observable<Object> {
        return this.http.post(this.baseUrl, entity);
    }
    update(id: number, entity: TheaterCreationDto): Observable<Object> {
        return this.http.put(`${this.baseUrl}/${id}`, entity);
    }
    delete(id: number): Observable<Object> {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
}
