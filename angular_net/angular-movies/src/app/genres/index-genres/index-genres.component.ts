import {Component, inject} from '@angular/core';
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {RouterLink} from "@angular/router";
import {GenresService} from "../genres.service";
import {GenreDTO} from "../genres.models";
import {GenericListComponent} from "../../shared/components/generic-list/generic-list.component";
import {MatTableModule} from "@angular/material/table";
import {MatPaginatorModule, PageEvent} from "@angular/material/paginator";
import {PaginationDTO} from "../../shared/models/pagination.model";
import {HttpResponse} from "@angular/common/http";
import {SwalDirective} from "@sweetalert2/ngx-sweetalert2";

@Component({
    selector: 'app-index-genres',
    imports: [MatButtonModule, MatIconModule, RouterLink, GenericListComponent, MatTableModule, MatPaginatorModule, SwalDirective],
    templateUrl: './index-genres.component.html',
    styleUrl: './index-genres.component.css'
})
export class IndexGenresComponent {
    genresService = inject(GenresService);
    genres!: GenreDTO[];
    columnsToDisplay = ["id", "name", "actions"];
    pagination: PaginationDTO = {page: 1, recordsPerPage: 5};
    totalRecordsCount: number = 0;
    swalParams = {
        title: "Confirmation",
        text: "Are you sure you want to delete this record?",
        showCancelButton: true,
    }
    
    constructor() {
        this.loadRecords();
    }
    
    loadRecords() {
        this.genresService.getPaginated(this.pagination).subscribe((response: HttpResponse<GenreDTO[]>)=> {
            this.genres = response.body as GenreDTO[];
            const header = response.headers.get("total-records-count") as string;
            this.totalRecordsCount = parseInt(header, 10);
        });
    }
    
    updatePagination(event: PageEvent) {
        this.pagination = {page: event.pageIndex + 1, recordsPerPage: event.pageSize};
        this.loadRecords();
    }
    
    delete(id: number) {
        this.genresService.delete(id).subscribe(() => {
           this.loadRecords(); 
        });
    }
}
