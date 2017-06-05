import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Vehicle, KeyValuePair } from './../../models/vehicle';
import { VehicleService } from './../../services/vehicle.service';

@Component({
    selector: 'vehicle-list',
    templateUrl: 'vehicle-list.component.html'
})

export class VehicleListComponent implements OnInit {
    private readonly PAGE_SIZE=3;
    public queryResult:any={};
    public makes:any[];
    public models: any[];
    public query: any={
        page:1,
        pageSize:this.PAGE_SIZE
    };
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
        this.query.page=1;
        this.pupulateVehicules();
    }

    private pupulateVehicules(){
        this.vehicleService.getVehicles(this.query)
            .subscribe(result => this.queryResult =  result);
    }

    resetFilter(){
        this.query = {
            page:1,
            pageSize:this.PAGE_SIZE
        };
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

    onPageChange(page){
        this.query.page = page;
        this.pupulateVehicules();
    }
}

interface Query{
    makeId:number,
    modelId:number,
    sortBy:string,
    isSortAscending:boolean
}