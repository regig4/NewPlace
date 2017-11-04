export interface Representation<T> {         // TODO: think, research: is 'export' make sense in this case  
    resource: T;
    links: object[];
}
