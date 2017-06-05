import { Vehicle, SaveVehicle } from './../models/vehicle';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class VehicleService {

    constructor(private http: Http) {

    }

    getMakes() {
        return this.http.get('/api/makes')
            .map(res => res.json());
    }

    getFeatures() {
        return this.http.get('/api/features')
            .map(res => res.json());
    }

    create(vehicle) {
        return this.http.post('/api/vehicles', vehicle)
            .map(res => res.json());
    }
    getVehicles(filter){
        return this.http.get('/api/vehicles/GetVehicles?' + this.toQueryString(filter))
            .map(res => res.json());        
    }
    getVehicle(id) {
        return this.http.get('/api/vehicles/' + id)
            .map(res => res.json());

    }

    toQueryString(obj){
        var parts=[];
        for(var prop in obj){
            var value = obj[prop];
            if (value != null && value != undefined)
                parts.push(encodeURIComponent(prop) + '=' + encodeURIComponent(value));
        }

        return parts.join('&');
    }

    update(vehicle : SaveVehicle) {
        return this.http.put('/api/vehicles/' + vehicle.id, vehicle)
            .map(res => res.json());
    }

    delete(id) {
        return this.http.delete('/api/vehicles/' + id)
            .map(res => res.json());
    }
}