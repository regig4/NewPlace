import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, ParamMap } from "@angular/router";
import { AdvertisementsService } from "../advertisements/advertisements.service";
import { Advertisement } from "../advertisements/advertisement";
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/map';

@Component({
    templateUrl: "./details.component.html",
    selector: "advertisement-details"
})
export class DetailsComponent implements OnInit {
    advertisement: Advertisement;

    constructor(private route: ActivatedRoute, private service: AdvertisementsService)
    { }

    ngOnInit(): void {
        this.route.paramMap.switchMap(
            (params: ParamMap) => this.service.getAdvertisement(Number(params.get('id')))).subscribe(a => this.advertisement = a);
    }

}