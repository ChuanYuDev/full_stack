import { Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { IndexGenresComponent } from './genres/index-genres/index-genres.component';

// Define as many routes as I want
//      A route is basically the combination of a URL and a component
export const routes: Routes = [
    {path: '', component: LandingPageComponent},
    {path: 'genres', component: IndexGenresComponent}
];
