export interface Representation<T>{         // is 'export' make sense  
    resource: T;
    links: object[];
}

export class AdvertisementRepresentation implements Representation<Advertisement> {
    resource: Advertisement;
    thumbnail: string;
    links: object[];
}

export class Advertisement {
    id: number;
    category: object;
    createDate: string;
    validityTime: string;
    user: string;
    apartmentAddress: string;
    price: number;
    provision: number;
    totalCost: number;
    utilitesCost: object[];
    thumbnail: string;
    links: object[];
}
