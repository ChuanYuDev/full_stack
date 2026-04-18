import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class SecurityService {

    constructor() { }
    
    isLoggedIn(): boolean {
        return true;
    }
    
    getRole(): string {
        return "nonAdmin";
        // return "admin";
    }
}
