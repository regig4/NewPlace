import { Component } from '@angular/core';
import { PaymentConfirmation } from '../shared/models/payment-confirmation';
import { PaymentService } from '../shared/services/payment.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private paymentService: PaymentService) { }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  onDonate() {
    const amount: string | null = prompt("How much money (USD) you want to donate?");
    if (/^\d+$/.test(amount as string)) {
      this.paymentService.donate(amount as unknown as number).then(result =>
        alert("Thank you for donation! You donated " + (result as PaymentConfirmation).amount +
          "USD. Your payment id is " + (result as PaymentConfirmation).paymentId + ".")
      );
    }
    else {
      alert("You need to enter number");
    }
  }
}
