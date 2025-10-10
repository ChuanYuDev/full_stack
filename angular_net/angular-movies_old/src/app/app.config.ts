import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';

import { routes } from './app.routes';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field'
import { provideMomentDateAdapter } from '@angular/material-moment-adapter';

export const appConfig: ApplicationConfig = {
    providers: [
        provideZoneChangeDetection({ eventCoalescing: true }),

        provideRouter(routes, withComponentInputBinding()),

        {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {subscriptSizing: 'dynamic'}},

        // Provide labels that we will use in a calendar component that comes with Angular material
        //      This configuration is going to be global for our whole application
        provideMomentDateAdapter({
            // We are able to change the format of date of birth using dateInput field in parse and display
            parse: {
                dateInput: ['DD-MM-YYYY'],
                // dateInput: ['MM-DD-YYYY'],
            },
            display: {
                dateInput: 'DD-MM-YYYY',
                // dateInput: 'MM-DD-YYYY',

                // monthYearLabel refers to the label of datepicker at the top left corner
                monthYearLabel: 'MMM YYYY',
                // monthYearLabel: 'MMM/YYYY',
                dateA11yLabel: 'LL',
                monthYearA11yLabel: 'MMMM YYYY',
            },
        }),
    ]
};
