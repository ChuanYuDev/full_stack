import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {SecurityService} from "../../security/security.service";

export const isAdminGuard: CanActivateFn = (route, state) => {
    const securityService = inject(SecurityService);
    const router = inject(Router);
    
    if (securityService.getRole() === "admin") {
        return true;
    } 
    
    return router.parseUrl("/login");
};
