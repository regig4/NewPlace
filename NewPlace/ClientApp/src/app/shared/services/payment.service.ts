import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { PaymentConfirmation } from '../models/payment-confirmation';

@Injectable()
export class PaymentService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  donate(amount: number): Promise<PaymentConfirmation | undefined> {
    return this.http.get(this.baseUrl + 'api/Payment?userId=1&amount=' + amount +'&currency="USD"').toPromise().then(
      result => {
        console.log(result);
        return result as PaymentConfirmation;
      }
    );
  }
}
