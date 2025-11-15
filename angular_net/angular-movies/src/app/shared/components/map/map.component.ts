import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {icon, latLng, LeafletMouseEvent, marker, Marker, tileLayer} from "leaflet";
import {LeafletModule} from "@bluehalo/ngx-leaflet";
import {Coordinate} from "./coordinate.model";

@Component({
    selector: 'app-map',
    imports: [LeafletModule],
    templateUrl: './map.component.html',
    styleUrl: './map.component.css'
})
export class MapComponent implements OnInit{

    @Input()
    initialCoordinates: Coordinate[] = [];

    @Output()
    selectedCoordinate = new EventEmitter<Coordinate>();

    options = {
        layers: [
            tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                maxZoom: 18,
                attribution: '...'
            })
        ],
        zoom: 14,
        center: latLng(49.25535155016482, -123.10423437628756)
    };

    markerOptions = {
        icon: icon({
            iconSize: [25, 41],
            iconAnchor: [13, 41],
            iconUrl: "assets/marker-icon.png",
            iconRetinaUrl: "assets/marker-icon-2x.png",
            shadowUrl: "assets/marker-shadow.png"
        })
    };

    layers: Marker<any>[] = [];

    ngOnInit(): void {
        this.layers = this.initialCoordinates.map(coordinate => {
            return marker([coordinate.latitude, coordinate.longitude], this.markerOptions);
        });
    }

    handleClick(event: LeafletMouseEvent) {
        const latitude = event.latlng.lat;
        const longitude = event.latlng.lng;

        this.layers = [];
        this.layers.push(marker([latitude, longitude], this.markerOptions));

        const coordinate: Coordinate = {latitude: latitude, longitude: longitude};
        this.selectedCoordinate.emit(coordinate);
    }

    // TO DO
    // Leaflet +/- icon is in the menu component
    // Error message for map component?
    // TheaterCreationDTO contains coordinate property
    // Marker(model coordinate) may be out of the range of the initial centered image
}
