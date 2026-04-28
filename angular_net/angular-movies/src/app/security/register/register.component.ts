import {Component, inject} from '@angular/core';
import {AuthenticationFormComponent} from "../authentication-form/authentication-form.component";
import {SecurityService} from "../security.service";
import {UserCredentialsDto} from "../security.models";
import {Router} from "@angular/router";
import {extractIdentityErrors} from "../../shared/functions/extractErrors";
import {DisplayErrorsComponent} from "../../shared/components/display-errors/display-errors.component";

@Component({
    selector: 'app-register',
    imports: [AuthenticationFormComponent, DisplayErrorsComponent],
    templateUrl: './register.component.html',
    styleUrl: './register.component.css'
})
export class RegisterComponent {
    securityService = inject(SecurityService);
    router = inject(Router);
    errors: string[] = [];
    
    register(userCredentialsDto: UserCredentialsDto) {
        this.securityService.register(userCredentialsDto).subscribe({
            next: () => {
                this.router.navigate(["/"]);
            },
            
            error: err => {
                this.errors = extractIdentityErrors(err);
            }
        });
    }
}
