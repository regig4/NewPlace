import { Representation } from "./representation";
import { User } from "../user";

export class UserRepresentation implements Representation<User> {
    links: object[] | undefined;
    resource: User | undefined;
    password: string = "";
    token: string = "";
}