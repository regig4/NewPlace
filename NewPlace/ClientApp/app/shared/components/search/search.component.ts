import { Component, Output, EventEmitter } from '@angular/core';
import { AdvertisementsService } from '../../services/advertisements.service';
import { Advertisement } from '../../models/advertisement';

@Component({
    selector: 'mySearch',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css'],
    //providers: [AdvertisementsService]                  // TODO: why is it necessary? there is declaration for root module
})
export class SearchComponent {
    searched = false;
    @Output() notify: EventEmitter<Advertisement[]> = new EventEmitter<Advertisement[]>();

    constructor(private service: AdvertisementsService) { }

    public search() {
        //this.http.get(this.baseUrl + "/search?city=krakow&estateType=flat");
        this.service.getAdvertisements().then(result => {
            this.notify.emit(result);
            this.searched = true;
        });
    }
}
