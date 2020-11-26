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
  imgFile: File;
  base64: string | ArrayBuffer;

  constructor(private service: AdvertisementsService) { 
    this.advertisement = new Advertisement();
  }

  ngOnInit() {
  }

  onSubmit() {
    if (this.advertisement.id === 0)
      this.advertisement.id = null;

    this.advertisement.userName = "Test user";
    this.advertisement.utilities = [];
    this.advertisement.thumbnail = {
      resource: this.base64,
      mediaType: 'image/base64',
      links: null
    }

    this.service.addAdvertisement(this.advertisement);
  }

  onAddUtilityClick() {
    if (!this.advertisement.utilitesCost) {
      this.advertisement.utilitesCost = [];
    }
    this.advertisement.utilitesCost.push({ name: "TEST", price: 100 });
  }

  handleFileInput(files: FileList) {
    this.imgFile = files.item(0);
    const fileReader = new FileReader();
    fileReader.onloadend = e => this.base64 = fileReader.result;
    fileReader.readAsDataURL(this.imgFile);
  }

}
