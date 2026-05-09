import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpResponse} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {AuthenticationResponseDto, UserCredentialsDto, UserDto} from "./security.models";
import {Observable, tap} from "rxjs";
import {PaginationDTO} from "../shared/models/pagination.model";
import {buildQueryParams} from "../shared/functions/buildQueryParams";

@Injectable({
    providedIn: 'root'
})
export class SecurityService {

    private http = inject(HttpClient);
    private baseUrl = environment.apiUrl + "/users";
    
    private readonly keyToken = "token";
    private readonly keyExpiration = "token-expiration";
    
    constructor() { }
    
    register(userCredentialsDto: UserCredentialsDto) {
        return this.http.post<AuthenticationResponseDto>(`${this.baseUrl}/register`, userCredentialsDto)
            .pipe(
                tap(authenticationResponseDto => {
                    this.storeToken(authenticationResponseDto);
                })
            );
    }

    login(userCredentialsDto: UserCredentialsDto) {
        return this.http.post<AuthenticationResponseDto>(`${this.baseUrl}/login`, userCredentialsDto)
            .pipe(
                tap(authenticationResponseDto => {
                    this.storeToken(authenticationResponseDto);
                })
            );
    }
    
    logout() {
        window.localStorage.removeItem(this.keyToken);
        window.localStorage.removeItem(this.keyExpiration);
    }
    
    isLoggedIn(): boolean {
        const token = this.getJwtToken();
        
        if (!token) {
            return false;
        }
        
        const expiration = window.localStorage.getItem(this.keyExpiration);
        
        if (!expiration) {
            return true;
        }
        
        const expirationDate = new Date(expiration);
        
        if (expirationDate <= new Date())
        {
            this.logout();
            return false;
        }
        
        return true;
    }

    getJwtToken(): string | null {
        return window.localStorage.getItem(this.keyToken);
    }

    getJwtClaim(field: string): string {
        const token = this.getJwtToken();

        if (!token) {
            return "";
        }

        const claims = JSON.parse(window.atob(token.split(".")[1]));

        return claims[field];
    }

    getRole(): string {
        const isAdmin = this.getJwtClaim("isadmin");
        
        if (isAdmin)
        {
            return "admin";
        }
        
        return "nonAdmin";
    }

    getUsersPaginated(paginationDto: PaginationDTO): Observable<HttpResponse<UserDto[]>>{
        const queryParams = buildQueryParams(paginationDto);
        return this.http.get<UserDto[]>(`${this.baseUrl}/users-list`, {params: queryParams, observe: "response"});
    }
    makeAdmin(email: string) {
        return this.http.post(`${this.baseUrl}/make-admin`, {email});
    } 
    
    removeAdmin(email: string) {
        return this.http.post(`${this.baseUrl}/remove-admin`, {email});
    }
    
    private storeToken(authenticationResponseDto: AuthenticationResponseDto) {
        window.localStorage.setItem(this.keyToken, authenticationResponseDto.token);
        
        if (authenticationResponseDto.expiration) {
            window.localStorage.setItem(this.keyExpiration, authenticationResponseDto.expiration.toString());
        }
    }
}
