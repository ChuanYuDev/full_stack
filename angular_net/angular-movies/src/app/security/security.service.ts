import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {AuthenticationResponseDto, UserCredentialsDto} from "./security.models";
import {tap} from "rxjs";

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
    
    getJwtClaim(field: string): string {
        const token = this.getJwtToken();
        
        if (!token) {
            return "";
        } 
        
        const claims = JSON.parse(window.atob(token.split(".")[1]));
        
        return claims[field];
    }
    
    getRole(): string {
        // return "nonAdmin";
        return "admin";
    }
    
    private storeToken(authenticationResponseDto: AuthenticationResponseDto) {
        window.localStorage.setItem(this.keyToken, authenticationResponseDto.token);
        
        if (authenticationResponseDto.expiration) {
            window.localStorage.setItem(this.keyExpiration, authenticationResponseDto.expiration.toString());
        }
    }
    
    private getJwtToken(): string | null {
        return window.localStorage.getItem(this.keyToken);
    }
    
    private logout() {
        window.localStorage.removeItem(this.keyToken);
        window.localStorage.removeItem(this.keyExpiration);
    }
}
