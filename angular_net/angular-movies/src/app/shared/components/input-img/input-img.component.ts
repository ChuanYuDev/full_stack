import {Component, EventEmitter, Input, Output} from '@angular/core';
import {MatButtonModule} from "@angular/material/button";
import {toBase64} from "../../functions/toBase64";

@Component({
    selector: 'app-input-img',
    imports: [MatButtonModule],
    templateUrl: './input-img.component.html',
    styleUrl: './input-img.component.css'
})
export class InputImgComponent {
    @Input({required: true})
    title!: string;
    
    @Input()
    imageURL?: string;
    
    imageBase64?: string;
    
    @Output()
    selectedFile = new EventEmitter<File>();
    
    change(event: Event) {
        const input = event.target as HTMLInputElement;
        const fileList = input.files;
        
        if (fileList && fileList.length > 0) {
            const file: File = fileList[0];
            
            toBase64(file)
                .then((value: string)=> this.imageBase64 = value)
                .catch((error)=> console.log(error));
            
            this.selectedFile.emit(file);
            this.imageURL = undefined;
        } 
    }
}
