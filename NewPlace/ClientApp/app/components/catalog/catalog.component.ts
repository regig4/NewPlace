import { Component, Inject } from '@angular/core';
import { Advertisement } from '../../shared/models/advertisement';
import { Representation } from '../../shared/models/representations/representation';
import { AdvertisementsService } from '../../shared/services/advertisements.service';

@Component({
    selector: 'advertisements',
    templateUrl: './catalog.component.html'
})
export class AdvertisementsComponent {
    public Advertisements: Advertisement[];

    constructor(private service: AdvertisementsService) {
    }

    public onSearchResults(results: Advertisement[]) {
        this.Advertisements = results;
    }
}

