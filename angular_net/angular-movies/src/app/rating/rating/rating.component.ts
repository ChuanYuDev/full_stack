import {Component, EventEmitter, inject, Input, OnInit, Output} from '@angular/core';
import {MatIconModule} from "@angular/material/icon";
import {NgClass} from "@angular/common";
import {SecurityService} from "../../security/security.service";
import Swal from "sweetalert2";

@Component({
    selector: 'app-rating',
    imports: [MatIconModule, NgClass],
    templateUrl: './rating.component.html',
    styleUrl: './rating.component.css'
})
export class RatingComponent implements OnInit {
    securityService = inject(SecurityService);
    
    @Input({required: true, transform: (value: number) => Array(value).fill(0)})
    maxRating: any[] = [];
    
    @Input()
    selectedRating: number = 0;
    
    clickedRating: number = 0;
    
    @Output()
    rated = new EventEmitter<number>();

    ngOnInit(): void {
        this.clickedRating = this.selectedRating;
    }
    
    handleMouseEnter(index: number){
        this.selectedRating = index + 1;
    }
    
    handleMouseLeave() {
        if (this.clickedRating !== 0) {
            this.selectedRating = this.clickedRating;
        } else {
            this.selectedRating = 0;
        }
    }
    
    handleClick(index: number) {

        if (!this.securityService.isLoggedIn()) {
            Swal.fire("Oops", "You must register or login first", "warning");
            return;
        }

        this.clickedRating = index + 1;
        this.selectedRating = this.clickedRating;
        this.rated.emit(this.selectedRating);
    }
}
