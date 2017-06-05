import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Vehicle, Contact } from './../../models/vehicle';
import { ToastyService } from "ng2-toasty";
import { VehicleService } from './../../services/vehicle.service';

@Component({
    selector: 'viewVehicle',
    templateUrl: 'view-vehicle.html'
})

export class ViewVehiculeComponent implements OnInit {
    vehicle: any = {
        make:{},
        model:{},
        feature:{},
        contactName:'',
        contactPhone:'',
        contactEmail:''
    };
    vehicleId: number;
    photos:any={};
    constructor(private route: ActivatedRoute,
        private router: Router,
        private toastyService: ToastyService, private vehicleService: VehicleService) {

        route.params.subscribe(p => {
            this.vehicleId = +p['id'];
            if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
                router.navigate(['/vehicles']);
                return; 
            }
        });
    }

    ngOnInit() { 

    }

    delete(){

    }

    uploadPhoto(){

    }
}