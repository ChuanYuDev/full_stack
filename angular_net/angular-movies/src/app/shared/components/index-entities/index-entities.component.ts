import {Component, inject, Input} from '@angular/core';
import {CRUD_SERVICE_TOKEN} from "../../providers/providers";
import {ICRUDService} from "../../interfaces/ICRUDService";
import {PaginationDTO} from "../../models/pagination.model";
import {HttpResponse} from "@angular/common/http";
import {MatPaginatorModule, PageEvent} from "@angular/material/paginator";
import {RouterLink} from "@angular/router";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {GenericListComponent} from "../generic-list/generic-list.component";
import {MatTableModule} from "@angular/material/table";
import {SwalDirective} from "@sweetalert2/ngx-sweetalert2";

@Component({
    selector: 'app-index-entities',
    imports: [RouterLink, MatButtonModule, MatIconModule, GenericListComponent, MatTableModule, SwalDirective, MatPaginatorModule],
    templateUrl: './index-entities.component.html',
    styleUrl: './index-entities.component.css'
})
export class IndexEntitiesComponent<TDTO, TCreationDTO> {
    CRUDService = inject(CRUD_SERVICE_TOKEN) as ICRUDService<TDTO, TCreationDTO>;
    entities!: TDTO[];
    
    pagination: PaginationDTO = {page: 1, recordsPerPage: 5};
    totalRecordsCount: number = 0;

    swalParams = {
        title: "Confirmation",
        text: "Are you sure you want to delete this record?",
        showCancelButton: true,
    };

    @Input({required: true})
    title?: string;
    
    @Input({required: true})
    createRoute?: string;

    @Input({required: true})
    editRoute?: string;
    
    @Input()
    columnsToDisplay = ["id", "name", "actions"];
    
    constructor() {
        this.loadRecords();
    }

    loadRecords() {
        this.CRUDService.getPaginated(this.pagination).subscribe((response: HttpResponse<TDTO[]>)=> {
            this.entities = response.body as TDTO[];
            const header = response.headers.get("total-records-count") as string;
            this.totalRecordsCount = parseInt(header, 10);
        });
    }

    updatePagination(event: PageEvent) {
        this.pagination = {page: event.pageIndex + 1, recordsPerPage: event.pageSize};
        this.loadRecords();
    }

    delete(id: number) {
        this.CRUDService.delete(id).subscribe(() => {
            this.pagination.page = 1;
            this.loadRecords();
        });
    }
    
    firstLetterToUppercase(value: string) {
        if (!value) return value;
        
        return value.charAt(0).toUpperCase() + value.slice(1);
    }
}
