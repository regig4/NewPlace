import { Component, Output, EventEmitter } from '@angular/core';
import { AdvertisementsService } from '../../services/advertisements.service';
import { Advertisement } from '../../models/advertisement';
import { RecomendationService } from '../../services/recomendations.service';

@Component({
    selector: 'mySearch',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent {
    searched = false;
    @Output() notify: EventEmitter<Advertisement[]> = new EventEmitter<Advertisement[]>();
    
    estateTypes = ["room", "flat", "house"];

    estateTypeSelected: string = "";
    city: string = "";
    msg:string = "";

    constructor(private service: AdvertisementsService, private recomendationService: RecomendationService) { }

    public search() {

        this.recomendationService.ConnectionTest((message: string) => {
                this.msg = message;
            }
        );

        this.service.getByFilter(this.estateTypeSelected, this.city, 0).then(result => {
            this.notify.emit(result);
            this.searched = true;
        });
    }
}
