import {Component, inject, OnInit} from '@angular/core';
import {FormBuilder, ReactiveFormsModule} from "@angular/forms";
import {MatFormField, MatInputModule} from "@angular/material/input";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatCheckboxModule} from "@angular/material/checkbox";

@Component({
    selector: 'app-patch-change-values',
    imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatCheckboxModule],
    templateUrl: './patch-change-values.component.html',
    styleUrl: './patch-change-values.component.css'
})
export class PatchChangeValuesComponent implements OnInit{
    private formBuilder = inject(FormBuilder);
    
    form = this.formBuilder.group({
        name: [""],
        emit: false
    });
    
    formToBePatched = this.formBuilder.group({
        name: [""],
    });

    ngOnInit(): void {
        this.formToBePatched.valueChanges.subscribe(value => {
           console.log("Name to be patched: ", value); 
        });
    }
    
    patchName() {
        const value = this.form.controls.name.value;
        const emit = this.form.controls.emit.value || false;
        console.log("emit: ", emit);
        this.formToBePatched.patchValue({name: value}, {emitEvent: emit});
    }
}
