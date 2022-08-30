export class Advertisement {
  id: number | null = 0;
  title = "";
  estateType: string | undefined;
  pricingType: string | undefined;
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
  utilities: object[] | undefined;
  utilitesCost: object[] | undefined;
  thumbnail: any;
  links: object[] | undefined;
}
