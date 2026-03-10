import {AfterViewInit, Component, ComponentRef, inject, Input, ViewChild, ViewContainerRef} from '@angular/core';
import {LoadingComponent} from "../loading/loading.component";
import {DisplayErrorsComponent} from "../display-errors/display-errors.component";
import {CRUD_SERVICE_TOKEN} from "../../providers/providers";
import {ICRUDService} from "../../interfaces/ICRUDService";
import {Router} from "@angular/router";
import {extractErrors} from "../../functions/extractErrors";

@Component({
    selector: 'app-edit-entity',
    imports: [LoadingComponent, DisplayErrorsComponent],
    templateUrl: './edit-entity.component.html',
    styleUrl: './edit-entity.component.css'
})
export class EditEntityComponent<TDTO, TCreationDTO> implements AfterViewInit{
    CRUDService = inject(CRUD_SERVICE_TOKEN) as ICRUDService<TDTO, TCreationDTO>;
    router = inject(Router);
    
    loading: boolean = true;
    errors: string[] = [];
    
    @Input({required: true})
    title?: string;
    
    @Input({required: true})
    id: number = 0;
    
    @Input({required: true})
    formComponent: any;
    
    @Input({required: true})
    indexRoute?: string;
    
    @ViewChild("contentForm", {read: ViewContainerRef})
    contentForm?: ViewContainerRef;
    
    private componentRef?: ComponentRef<any>;
    
    ngAfterViewInit(): void {
        this.CRUDService.getById(this.id).subscribe((model: TDTO) => {
            this.loadComponent(model);
        });
    }
    
    loadComponent(model: any) {
        if (this.contentForm) {
            this.componentRef = this.contentForm.createComponent(this.formComponent);
            
            this.componentRef.instance.model = model;
            this.componentRef.instance.postForm.subscribe((entity: TCreationDTO) => {
                this.saveChanges(entity);
            });
            
            setTimeout(() => {
                this.loading = false;
            });
        }
    }
    
    saveChanges(entity: TCreationDTO) {
        this.CRUDService.update(this.id, entity).subscribe({
           next: () => {
               this.router.navigate([this.indexRoute]);
           },
           
           error: (err) => {
               this.errors = extractErrors(err);
           }
        });
    }
}
