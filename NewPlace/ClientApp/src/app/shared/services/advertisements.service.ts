import { Injectable, Inject } from '@angular/core';
import { Advertisement } from '../../shared/models/advertisement';
import { Representation } from '../../shared/models/representations/representation';
import { AdvertisementRepresentation } from '../../shared/models/representations/advertisementRepresentation';
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable()
export class AdvertisementsService {

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

    getAdvertisement(id: number): Promise<Advertisement | undefined> {
      return this.http.get(this.baseUrl + 'api/Advertisement/' + id).toPromise().then(
        result => {
          let r = (result as Representation<Advertisement>).resource;
          console.log(r);
          r.thumbnail = result["thumbnail"];
          console.log(result);

          return r;
        }
        );
    }

    getAdvertisements(): Promise<Advertisement[]> {
        let advertisements: Advertisement[] = []; 
        this.http.get(this.baseUrl + 'api/Advertisement').subscribe(result => {
            const represtentations = result as AdvertisementRepresentation[];
            advertisements = this.unpackResponse(represtentations);
        }, error => console.error(error));
        return Promise.resolve(advertisements);
    }

    getPromoted(count: number): Promise<Advertisement[]> {
        let promoted: Advertisement[] = [];
        this.http.get(this.baseUrl + 'api/Promoted/' + count).toPromise().then(
            result => promoted = this.unpackResponse(result as AdvertisementRepresentation[]));
        return Promise.resolve(promoted);
    }

    getByFilter(estateType: string, city: string, radius: number): Promise<Advertisement[]> {
        return this.http.get(this.baseUrl + "api/advertisement/search?city=" +  city + "&estateType=" + estateType + "&radius=" + radius).toPromise().then(result =>
            this.unpackResponse(result as AdvertisementRepresentation[]));
  }

  addAdvertisement(advertisement: Advertisement) {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    const body = { resource: advertisement };
    const bodyJSON = JSON.stringify(body);
    this.http.post<Advertisement>(this.baseUrl + "api/Advertisement", bodyJSON, options).subscribe(
      (data: Advertisement) => console.log(data), // success path
      error => console.log(error) // error path
    );;
  }

    private unpackResponse(response: AdvertisementRepresentation[]): Advertisement[] {
        let result: Advertisement[] = [];
        for (let r of response) {
            if(!r.resource)
                continue;
            result.push(r.resource);
            result[result.length - 1].links = r.links;

            result[result.length - 1].thumbnail = r.thumbnail ? r.thumbnail : "";
        }
        return result;
    }
}
