import { Injectable, Inject } from "@angular/core";
import { HubConnection, HubConnectionBuilder, LogLevel, JsonHubProtocol, HttpTransportType } from "@aspnet/signalr";
import { Http, RequestOptions, Headers } from "@angular/http";
import { UserRepresentation } from '../models/representations/userRepresentation';
import { User } from "../models/user";
import { ReadPropExpr } from "../../../../node_modules/@angular/compiler";

@Injectable()
export class RecomendationService {
    private _hubConnection: HubConnection;

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        let usr = new User();
        usr.email = "aa";
        usr.id = 34;
        usr.login = "aa";

        let usrRepr = new UserRepresentation();
        usrRepr.links = [{ 'aaa': "bbb" }];
        usrRepr.password = "aa";
        usrRepr.resource = usr;

        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        this._hubConnection = new HubConnectionBuilder()         // TODO: inject dependecies
            .configureLogging(LogLevel.Trace)
            .withUrl("http://localhost:5000/recommendationHub",
                {
                    accessTokenFactory: async () => {
                        let token = localStorage.getItem('token');

                        if (!token) {
                            let response = await this.http.post(this.baseUrl + 'Authorization/Login', JSON.stringify(usrRepr),
                                {
                                    headers: headers

                                }).toPromise();
                            let repr: UserRepresentation = (response.json() as UserRepresentation);
                            localStorage.setItem('token', repr.token);
                            return repr.token;
                        }
                        
                        return token;
                    }
                })
            .withHubProtocol(new JsonHubProtocol())
            .build();

    }

    public ConnectionTest(cb: (message: string) => void) {
        this._hubConnection.on("ReceiveMessage", (msg: string) => {
            console.log(msg);
            cb(msg);
        });
        let promise = this._hubConnection.start();
        promise.catch(err => console.error(err));
        promise.then(x => this._hubConnection.invoke("RecommendTest").catch(err => console.error(err)));
    }
}