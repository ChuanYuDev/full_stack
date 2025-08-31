import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';

import { routes } from './app.routes';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field'

export const appConfig: ApplicationConfig = {
    providers: [
        provideZoneChangeDetection({ eventCoalescing: true }),

        // withComponentInputBinding
        //      Enables binding information from the Router state directly to the inputs of the component in Route configurations
        provideRouter(routes, withComponentInputBinding()),

        // Use dynamic sizing for our form field
        //      So that whenever we have to display a long error message, this is going to push down the content of what is below 
        {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {subscriptSizing: 'dynamic'}},
    ]
};
