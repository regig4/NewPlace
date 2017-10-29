import { Component } from '@angular/core';

@Component({
    selector: 'counter',
    templateUrl: './counter.component.html'
})
export class CounterComponent {
    public currentCount = 0;
    public Apartments = ['Apartment1', 'NewApartment', 'TestApartment'];

    public incrementCounter() {
        this.currentCount++;
    }
}
