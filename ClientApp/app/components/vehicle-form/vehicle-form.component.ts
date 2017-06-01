
import { ServerModule } from '@angular/platform-server';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VehicleService } from './../../services/vehicle.service';


@Component({
    selector: 'vehicleform',
    templateUrl: 'vehicle-form.component.html'
})

export class VehicleFormComponent implements OnInit {
    public makes: any[];
    public models: any[];
    public vehicle:any = {};
    public features: any[];

    constructor(private vehicleService: VehicleService) {

     }

    ngOnInit() {
        this.vehicleService.getMakes().subscribe(makes => this.makes = makes);
        this.vehicleService.getFeatures().subscribe(features => this.features = features);
    }
    
    onMakeChange(){
        var selectedMake = this.makes.find(m=>m.id == this.vehicle.make);
        this.models = selectedMake ? selectedMake.models : [];
    }
}