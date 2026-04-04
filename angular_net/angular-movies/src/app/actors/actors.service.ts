import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpResponse} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {ActorAutoCompleteDto, ActorCreationDto, ActorDto} from "./actors.models";
import {PaginationDTO} from "../shared/models/pagination.model";
import {Observable} from "rxjs";
import {buildQueryParams} from "../shared/functions/buildQueryParams";
import {ICRUDService} from "../shared/interfaces/ICRUDService";

@Injectable({
    providedIn: 'root'
})
export class ActorsService implements ICRUDService<ActorDto, ActorCreationDto>{

    constructor() { }
    
    private http = inject(HttpClient);
    private baseUrl = environment.apiUrl + "/actors";
    
    getPaginated(pagination: PaginationDTO): Observable<HttpResponse<ActorDto[]>> {
        const queryParams = buildQueryParams(pagination);
        return this.http.get<ActorDto[]>(this.baseUrl, {
            params: queryParams, 
            observe: "response"
        });
    }
    getById(id: number): Observable<ActorDto> {
        return this.http.get<ActorDto>(`${this.baseUrl}/${id}`);
    }
    
    getByName(name: string): Observable<ActorAutoCompleteDto[]> {
        return this.http.get<ActorAutoCompleteDto[]>(`${this.baseUrl}/${name}`);
    }
    
    create(actor: ActorCreationDto) {
        const formData = this.buildFormData(actor);
        
        return this.http.post(this.baseUrl, formData);
    }

    update(id: number, actor: ActorCreationDto) {
        const formData = this.buildFormData(actor);
        
        return this.http.put(`${this.baseUrl}/${id}`, formData);
    }

    delete(id: number) {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
    
    private buildFormData(actor: ActorCreationDto): FormData {
        const formData = new FormData();
        
        formData.append("name", actor.name);
        formData.append("dateOfBirth", actor.dateOfBirth.toISOString().split('T')[0]);
        
        if (actor.picture) {
            formData.append("picture", actor.picture);
        }
            
        return formData;
    }
}
