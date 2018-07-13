import { Component, Inject, OnInit } from '@angular/core';
import { Advertisement } from '../../shared/models/advertisement';
import { Representation } from '../../shared/models/representations/representation';
import { AdvertisementsService } from '../../shared/services/advertisements.service';
import { RecomendationService } from '../../shared/services/recomendations.service';

@Component({
    selector: 'advertisements',
    templateUrl: './catalog.component.html'
})
export class CatalogComponent {
    public advertisements: Advertisement[] | undefined;

    constructor(private service: AdvertisementsService) {
    }

    public onSearchResults(results: Advertisement[]) {
        this.advertisements = results;
    }
}

