import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
    providers: [
        provideZoneChangeDetection({ eventCoalescing: true }),

        // withComponentInputBinding
        //      Enables binding information from the Router state directly to the inputs of the component in Route configurations
        provideRouter(routes, withComponentInputBinding())
    ]
};
