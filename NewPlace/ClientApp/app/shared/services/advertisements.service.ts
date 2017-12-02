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
            advertisements = this.unpackResponse(represtentations);
        }, error => console.error(error));
        return Promise.resolve(advertisements);
    }

    getPromoted(count: number): Promise<Advertisement[]> {
        let promoted: Advertisement[] = [];
        this.http.get(this.baseUrl + 'api/Promoted/' + count).toPromise().then(
            result => promoted = this.unpackResponse(result.json() as AdvertisementRepresentation[]));
        return Promise.resolve(promoted);
    }

    getByFilter(estateType: string, city: string, radius: number): Promise<Advertisement[]> {
        return this.http.get(this.baseUrl + "api/advertisement/search?city=" + city + "&estateType=" + estateType + "&radius=" + radius).toPromise().then(result =>
            this.unpackResponse(result.json() as AdvertisementRepresentation[]));
    }

    private unpackResponse(response: AdvertisementRepresentation[]): Advertisement[] {
        let result: Advertisement[] = [];
        for (let r of response) {
            result.push(r.resource);
            result[result.length - 1].links = r.links;
            result[result.length - 1].thumbnail = r.thumbnail;
        }
        return result;
    }
}