import { Component } from '@angular/core';
import { Advertisement } from '../shared/models/advertisement';
import { AdvertisementsService } from '../shared/services/advertisements.service';

@Component({
    selector: 'advertisements',
    templateUrl: './catalog.component.html',
    styleUrls: ['./catalog.component.css']
})
export class CatalogComponent {
    public advertisements: Advertisement[] | undefined;

    constructor(private service: AdvertisementsService) {
    }

    public onSearchResults(results: Advertisement[]) {
        this.advertisements = results;
    }
}

