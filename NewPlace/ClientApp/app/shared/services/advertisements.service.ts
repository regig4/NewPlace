import { Injectable, Inject } from '@angular/core';
import { Advertisement } from '../../shared/models/advertisement';
import { Representation } from '../../shared/models/representations/representation';
import { AdvertisementRepresentation } from '../../shared/models/representations/advertisementRepresentation';
import { Http } from "@angular/http";
import "rxjs/add/operator/toPromise";

@Injectable()
export class AdvertisementsService {

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) { }

    getAdvertisement(id: number): Promise<Advertisement> {
        let r: Advertisement = new Advertisement();
        r.id = 6;
        return this.http.get(this.baseUrl + 'api/Advertisement/' + id).toPromise().then(
            result => (result.json() as Representation<Advertisement>).resource
        );
    }

    getAdvertisements(): Promise<Advertisement[]> {
        let advertisements: Advertisement[] = [];
        this.http.get(this.baseUrl + 'api/Advertisement').subscribe(result => {
            const represtentations = result.json() as AdvertisementRepresentation[];
            represtentations.forEach(r => {
                advertisements.push(r.resource);
                advertisements[advertisements.length - 1].links = r.links;
                advertisements[advertisements.length - 1].thumbnail = r.thumbnail;
            });
        }, error => console.error(error));
        return Promise.resolve(advertisements);
    }
}