import { Routes } from '@angular/router';
import {FloatComponent} from "./test/float/float.component";
import {PipeComponent} from "./test/pipe/pipe.component";
import {PatchChangeValuesComponent} from "./test/patch-change-values/patch-change-values.component";
import {CreateCardComponent} from "./test/content-projection/create-card/create-card.component";

export const routes: Routes = [
    {path: "create-card", component: CreateCardComponent},
    {path: "float", component: FloatComponent},
    {path: "patch-change-values", component: PatchChangeValuesComponent},
    {path: "pipe", component: PipeComponent},
];
