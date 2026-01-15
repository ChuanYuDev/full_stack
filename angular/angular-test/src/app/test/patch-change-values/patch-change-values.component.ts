import {Component, inject} from '@angular/core';
import {FormBuilder, ReactiveFormsModule} from "@angular/forms";

@Component({
    selector: 'app-patch-change-values',
    imports: [ReactiveFormsModule],
    templateUrl: './patch-change-values.component.html',
    styleUrl: './patch-change-values.component.css'
})
export class PatchChangeValuesComponent {
    private formBuilder = inject(FormBuilder);
    
    form = this.formBuilder.group({
        name: [""],
    });
}
