import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatHorizontalStepper } from '@angular/material/stepper';
import { Advertisement } from '../../models/advertisement';
import { AdvertisementsService } from '../../services/advertisements.service';

@Component({
  selector: 'app-advertisement-form',
  templateUrl: './advertisement-form.component.html',
  styleUrls: ['./advertisement-form.component.css']
})
export class AdvertisementFormComponent implements OnInit, AfterViewInit {

  advertisement: Advertisement;
  isValid = false;
  area = 0.5;
  imgFile: File | null = null;
  base64: string | ArrayBuffer = "";

  @ViewChild('stepper') stepper: MatHorizontalStepper | undefined;


  constructor(private service: AdvertisementsService) { 
    this.advertisement = new Advertisement();
  }

  ngOnInit() {
  }

  ngAfterViewInit() {
    (this.stepper as MatHorizontalStepper)._getIndicatorType = () => 'number';
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

  handleFileInput(element: EventTarget | null) {
    if ((element as HTMLInputElement) == null)
      return;
    this.imgFile = (<HTMLInputElement>element).files!.item(0) as File;
    const fileReader = new FileReader();
    fileReader.onloadend = e => this.base64 = fileReader.result as string;
    fileReader.readAsDataURL(this.imgFile);
  }

}
