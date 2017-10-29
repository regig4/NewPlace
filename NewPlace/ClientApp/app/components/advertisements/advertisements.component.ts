import { Component, Inject } from '@angular/core';
import { Advertisement, Representation } from './advertisement';
import { AdvertisementsService } from './advertisements.service';

@Component({
    selector: 'advertisements',
    templateUrl: './advertisements.component.html'
})
export class AdvertisementsComponent {
    public Advertisements: Advertisement[];

    constructor(private service: AdvertisementsService) {
        service.getAdvertisements().then(result => this.Advertisements = result);
    }
}

