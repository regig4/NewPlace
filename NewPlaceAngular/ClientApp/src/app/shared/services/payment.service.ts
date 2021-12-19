import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { PaymentConfirmation } from '../models/payment-confirmation';

@Injectable()
export class PaymentService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  donate(amount: number): Promise<PaymentConfirmation | undefined> {
    return this.http.post(this.baseUrl + 'api/Payment/donate',
      { userId: "068ff0b4-aee2-49b5-96e1-c30e2bf9141c", amount: amount, currency: "USD" }).toPromise().then(
      result => {
        console.log(result);
        return result as PaymentConfirmation;
      }
    );
  }
}
