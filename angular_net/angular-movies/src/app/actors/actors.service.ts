import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpResponse} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {ActorCreationDTO, ActorDTO} from "./actors.models";
import {PaginationDTO} from "../shared/models/pagination.model";
import {Observable} from "rxjs";
import {buildQueryParams} from "../shared/functions/buildQueryParams";

@Injectable({
    providedIn: 'root'
})
export class ActorsService {

    constructor() { }
    
    private http = inject(HttpClient);
    private baseURL = environment.apiURL + "/actors";
    
    getPaginated(pagination: PaginationDTO): Observable<HttpResponse<ActorDTO[]>> {
        const queryParams = buildQueryParams(pagination);
        return this.http.get<ActorDTO[]>(this.baseURL, {
            params: queryParams, 
            observe: "response"
        });
    }
    
    getById(id: number): Observable<ActorDTO> {
        return this.http.get<ActorDTO>(`${this.baseURL}/${id}`);
    }
    
    create(actor: ActorCreationDTO) {
        const formData = this.buildFormData(actor);
        
        return this.http.post(this.baseURL, formData);
    }
    
    update(id: number, actor: ActorCreationDTO) {
        const formData = this.buildFormData(actor);
        
        return this.http.put(`${this.baseURL}/${id}`, formData);
    }
    
    private buildFormData(actor: ActorCreationDTO): FormData {
        const formData = new FormData();
        
        formData.append("name", actor.name);
        formData.append("dateOfBirth", actor.dateOfBirth.toISOString().split('T')[0]);
        
        if (actor.picture) {
            formData.append("picture", actor.picture);
        }
            
        return formData;
    }
}
