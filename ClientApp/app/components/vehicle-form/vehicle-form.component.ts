import * as _ from 'underscore';
import { SaveVehicle, KeyValuePair, Vehicle } from './../../models/vehicle';
import { Inject } from '@angular/core';
import { ServerModule, PlatformState, INITIAL_CONFIG } from '@angular/platform-server';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from "@angular/router";
import { ToastyService } from "ng2-toasty";
import { Observable } from "rxjs/Observable";
import "rxjs/add/Observable/forkJoin";
import { VehicleService } from './../../services/vehicle.service';


@Component({
    selector: 'vehicleform',
    templateUrl: 'vehicle-form.component.html'
})

export class VehicleFormComponent implements OnInit {
    public makes: any[];
    public models: any[];
    public vehicle: SaveVehicle = {
        id: 0,
        makeId: 0,
        modelId: 0,
        isRegistered: false,
        features: [],
        contact: {
            name: '',
            phone: '',
            email: ''
        }
    };
    public features: any[];

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private toastyService: ToastyService, @Inject(VehicleService) private vehicleService: VehicleService) {


        route.params.subscribe(p => {
            this.vehicle.id = +p["id"];
        })
    }

    ngOnInit() {
        var sources = [
            this.vehicleService.getMakes(),
            this.vehicleService.getFeatures()
        ];

        if (this.vehicle.id)
            sources.push(this.vehicleService.getVehicle(this.vehicle.id));

        Observable.forkJoin(sources).subscribe(data => {
            this.makes = data[0];
            this.features = data[1];
            if (this.vehicle.id) {
                this.setVehicle(data[2]);
                this.populateModels();
            }
        }, err => {
            if (err.status == 404) {
                this.router.navigate(['/home']);
            }
        });

    }

    private setVehicle(v: Vehicle) {
        this.vehicle.id = v.id;
        this.vehicle.makeId = v.make.id;
        this.vehicle.modelId = v.model.id;
        this.vehicle.isRegistered = v.isRegistered;
        this.vehicle.features = _.pluck(v.features, 'id');
        this.vehicle.contact = v.contact;

    }

    private populateModels() {
        var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }

    onMakeChange() {
        this.populateModels();
        delete this.vehicle.modelId;
    }

    onFeatureToggle(id, $event) {
        if ($event.target.checked)
            this.vehicle.features.push(id);
        else {
            var index = this.vehicle.features.indexOf(id);
            this.vehicle.features.splice(index, 1);
        }
    }

    submit() {
        if(this.vehicle.id){
            this.vehicleService.update(this.vehicle)
                .subscribe(x => {
                    this.toastyService.success({
                        title:'Success',
                        msg:'The vehicle was sucessufully updated.',
                        theme:'bootstrap',
                        showClose:true,
                        timeout:5000
                    });
                });
        } else {
            this.vehicleService.create(this.vehicle)
                .subscribe(x => {
                    this.toastyService.success({
                        title:'Success',
                        msg:'The vehicle was sucessufully created.',
                        theme:'bootstrap',
                        showClose:true,
                        timeout:5000
                    });
                });
        }
    }

    delete(){
        if (confirm("Area you shure?")){
            this.vehicleService.delete(this.vehicle.id)
            .subscribe(x => {
                this.router.navigate(['/home']);
            });
        }
    }
}