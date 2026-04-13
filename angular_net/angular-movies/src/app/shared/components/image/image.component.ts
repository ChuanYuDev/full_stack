import {Component, Input} from '@angular/core';

@Component({
    selector: 'app-image',
    imports: [],
    templateUrl: './image.component.html',
    styleUrl: './image.component.css'
})
export class ImageComponent {
    @Input({required: true})
    src?: string;
    
    @Input()
    width?: string;
    
    @Input()
    height?: string;
    
    @Input()
    alt?: string;
}
