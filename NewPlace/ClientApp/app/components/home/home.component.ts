import { Component, Inject } from '@angular/core';
import { AdvertisementsService } from '../../shared/services/advertisements.service'
import { Advertisement } from '../../shared/models/advertisement';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
})
export class HomeComponent {
    searched = false;
    searchResults: Advertisement[];

    constructor(private service: AdvertisementsService) { }

    public search(estateType: string, city: string) {
        this.service.getByFilter(estateType, city, 0).then(result => {
            this.searchResults = result;
            this.searched = true;
        });
    }

    public onSearchResults(results: Advertisement[]) {
        this.searchResults = results;
        this.searched = true;
    }
}
