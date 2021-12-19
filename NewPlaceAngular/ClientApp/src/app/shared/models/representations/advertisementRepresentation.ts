import { Representation } from "./representation";
import { Advertisement } from "../advertisement";

export class AdvertisementRepresentation implements Representation<Advertisement> {
    resource: Advertisement | undefined;
    thumbnail: string | undefined;
    links: object[] | undefined;
}