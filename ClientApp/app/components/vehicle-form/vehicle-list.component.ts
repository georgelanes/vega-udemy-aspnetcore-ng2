import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Vehicle, KeyValuePair } from './../../models/vehicle';
import { VehicleService } from './../../services/vehicle.service';

@Component({
    selector: 'vehicle-list',
    templateUrl: 'vehicle-list.component.html'
})

export class VehicleListComponent implements OnInit {
    public vehicles: Vehicle[];
    public makes:any[];
    public models: any[];
    public query: any={};
    public columns = [
        {title:'Id'},
        {title:'Make', key:'make', isSortable:true},
        {title:'Model', key:'model', isSortable:true},
        {title:'Contact Name', key:'contactName', isSortable:true}
    ]
    

    constructor(private vehicleService: VehicleService) { }

    ngOnInit() { 
        this.vehicleService.getMakes()
            .subscribe(makes => this.makes = makes);

        this.pupulateVehicules();
    }

    private populateModels() {
        var selectedMake = this.makes.find(m => m.id == this.query.makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }


    onFilterChange(){
        this.populateModels();        
        this.pupulateVehicules();
    }

    private pupulateVehicules(){
        this.vehicleService.getVehicles(this.query)
            .subscribe(vehicle => this.vehicles =  vehicle);
    }

    resetFilter(){
        this.query = {};
        this.onFilterChange();
    }

    sortBy(colmnName){
        if (this.query.sortBy === colmnName){
            this.query.isSortAscending = !this.query.isSortAscending;
        } else {
            this.query.sortBy = colmnName;
            this.query.isSortAscending = true;
        }
        
        console.log(this.query);

        this.pupulateVehicules();
    }
}

interface Query{
    makeId:number,
    modelId:number,
    sortBy:string,
    isSortAscending:boolean
}