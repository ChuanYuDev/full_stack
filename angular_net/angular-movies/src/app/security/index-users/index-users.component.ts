import {Component, inject} from '@angular/core';
import {SecurityService} from "../security.service";
import {UserDto} from "../security.models";
import {PaginationDTO} from "../../shared/models/pagination.model";
import {HttpResponse} from "@angular/common/http";
import {GenericListComponent} from "../../shared/components/generic-list/generic-list.component";
import {MatTableModule} from "@angular/material/table";
import {MatButtonModule} from "@angular/material/button";
import {MatPaginatorModule, PageEvent} from "@angular/material/paginator";
import Swal from "sweetalert2";
import {AuthorizedComponent} from "../authorized/authorized.component";

@Component({
    selector: 'app-index-users',
    imports: [GenericListComponent, MatTableModule, MatButtonModule, MatPaginatorModule],
    templateUrl: './index-users.component.html',
    styleUrl: './index-users.component.css'
})
export class IndexUsersComponent {
    securityService = inject(SecurityService);
    
    paginationDto: PaginationDTO = {page: 1, recordsPerPage: 10};
    users: UserDto[] = [];
    totalRecordsCount: number = 0;
    columnsToDisplay = ["email", "actions"];
    
    constructor() {
        this.loadRecords();
    }
    
    loadRecords() {
        this.securityService.getUsersPaginated(this.paginationDto).subscribe((response: HttpResponse<UserDto[]>) => {
            this.users = response.body as UserDto[];
            const header = response.headers.get("total-records-count") as string;
            this.totalRecordsCount = parseInt(header, 10);
        });
    }

    updatePagination(event: PageEvent) {
        this.paginationDto = {page: event.pageIndex + 1, recordsPerPage: event.pageSize};
        this.loadRecords();
    }
    
    makeAdmin(email: string) {
        this.securityService.makeAdmin(email).subscribe(() => {
            Swal.fire("Successful", `The user ${email} is now an admin`, "success");
        });
    }
    
    removeAdmin(email: string) {
        this.securityService.removeAdmin(email).subscribe(() => {
           Swal.fire("Successful", `The user ${email} is not an admin anymore`, "success"); 
        });
    }
}
