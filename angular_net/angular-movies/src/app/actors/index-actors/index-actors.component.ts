import {Component, inject} from '@angular/core';
import {RouterLink} from "@angular/router";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {ActorsService} from "../actors.service";
import {ActorDTO} from "../actors.models";
import {PaginationDTO} from "../../shared/models/pagination.model";
import {HttpResponse} from "@angular/common/http";
import {MatPaginatorModule, PageEvent} from "@angular/material/paginator";
import {GenericListComponent} from "../../shared/components/generic-list/generic-list.component";
import {MatTableModule} from "@angular/material/table";
import {SwalDirective} from "@sweetalert2/ngx-sweetalert2";

@Component({
    selector: 'app-index-actors',
    imports: [MatButtonModule, MatIconModule, RouterLink, GenericListComponent, MatTableModule, SwalDirective, MatPaginatorModule],
    templateUrl: './index-actors.component.html',
    styleUrl: './index-actors.component.css'
})
export class IndexActorsComponent {
    actorsService = inject(ActorsService);
    actors!: ActorDTO[];
    columnsToDisplay = ["id", "name", "actions"];
    pagination: PaginationDTO = {page: 1, recordsPerPage: 5};
    totalRecordsCount: number = 0;
    swalParams = {
        title: "Confirmation",
        text: "Are you sure you want to delete this record?",
        showCancelButton: true
    };
    
    constructor() {
        this.loadRecords();
    }
    
    loadRecords() {
        this.actorsService.getPaginated(this.pagination).subscribe((response: HttpResponse<ActorDTO[]>)=> {
            this.actors = response.body as ActorDTO[];
            const header = response.headers.get("total-records-count") as string;
            this.totalRecordsCount = parseInt(header, 10);
        });
    }
    
    updatePagination(event: PageEvent){
        this.pagination = {page: event.pageIndex + 1, recordsPerPage: event.pageSize};
        this.loadRecords();
    }
}
