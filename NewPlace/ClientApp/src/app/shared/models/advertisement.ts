export class Advertisement {
  id = 0;
  title = "";
  estateType: string;
  pricingType: string;
  createDate = "";
  userName: string = "";
  estateArea: number = 0;
  estateAddress: string = "";
  estateCity: string = "";
  price: number = 0;
  provision: number = 0;
  private _totalCost: number = 0;
  get totalCost(): number {
    this._totalCost = +this.price + +this.provision; // todo add utilites cost
    return this._totalCost;
  }
  utilities: object[];
  utilitesCost: object[] | undefined;
  thumbnail: object | string;
  links: object[] | undefined;
}
