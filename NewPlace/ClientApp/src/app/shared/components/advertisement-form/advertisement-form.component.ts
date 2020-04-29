import { Component, OnInit } from '@angular/core';
import { Advertisement } from '../../models/advertisement';
import { AdvertisementsService } from '../../services/advertisements.service';

@Component({
  selector: 'app-advertisement-form',
  templateUrl: './advertisement-form.component.html',
  styleUrls: ['./advertisement-form.component.css']
})
export class AdvertisementFormComponent implements OnInit {

  advertisement: Advertisement;
  isValid = false;
  area = 0.5;

  constructor(private service: AdvertisementsService) {
    this.advertisement = new Advertisement();
  }

  ngOnInit() {
  }

  onSubmit() {
    this.service.addAdvertisement(this.advertisement);
  }

  onAddUtilityClick() {
    if (!this.advertisement.utilitesCost) {
      this.advertisement.utilitesCost = [];
    }
    this.advertisement.utilitesCost.push({ name: "TEST", price: 100 });
  }

}
