import { Component, Inject } from '@angular/core';
import { AdvertisementsService } from '../../components/advertisements/advertisements.service'
import { Advertisement } from '../advertisements/advertisement';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    //providers: [AdvertisementsService]                  // TODO: why is it necessary? there is declaration for root module
})
export class HomeComponent {
    searched = false;
    searchResults: Advertisement[];

    constructor(private service: AdvertisementsService) { }

    public search() {
        //this.http.get(this.baseUrl + "/search?city=krakow&estateType=flat");
        this.service.getAdvertisements().then(result => {
            this.searchResults = result;
            this.searched = true;
        });
    }
}
