export class Advertisement {
  id: number = 0;
  title: string = "";
  category: object | undefined;
  createDate: string = "";
  validityTime: string = "";
  userName: string = "";
  estateArea: string = "";
  estateAddress: string = "";
  estateCity: string = "";
  price: number = 0;
  provision: number = 0;
  private _totalCost: number = 0;
  get totalCost(): number {
    this._totalCost = +this.price + +this.provision; // todo add utilites cost
    return this._totalCost;
  }
  utilitesCost: object[] | undefined;
  thumbnail: string = "";
  links: object[] | undefined;
}
