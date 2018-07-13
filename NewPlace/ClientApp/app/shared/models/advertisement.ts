export class Advertisement {
    id: number = 0;
    title: string = "";
    category: object | undefined;
    createDate: string = "";
    validityTime: string = "";
    user: string = "";
    apartmentAddress: string = "";
    price: number = 0;
    provision: number = 0;
    totalCost: number = 0;
    utilitesCost: object[] | undefined;
    thumbnail: string = "";
    links: object[] | undefined;
}
