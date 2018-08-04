import { Injectable, Inject } from "@angular/core";
import { HubConnection, HubConnectionBuilder, LogLevel, JsonHubProtocol, HttpTransportType } from "@aspnet/signalr";
import { Http, Headers, RequestOptionsArgs } from "@angular/http";
import { UserRepresentation } from '../models/representations/userRepresentation';
import { User } from '../models/user';

@Injectable()
export class RecomendationService {
    private _hubConnection: HubConnection;

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {

        let usr: User = new User();
        usr.email = "aa";
        usr.id = 2;
        usr.login = "aa";

        let repr: UserRepresentation = 
        {
            links: [ {"link": "link" }],
            resource: {
                id: 2,
                login: "AA",
                email: "BB"
            },
            password: "CC",
            token: ""
        }

        this._hubConnection = new HubConnectionBuilder()         // TODO: inject dependecies
            .configureLogging(LogLevel.Trace)
            .withUrl("http://localhost:5000/recommendationHub",
                {
                    accessTokenFactory: async () : Promise<string> => {
                        let token = localStorage.getItem('token');
                        if(token)
                            return token;

                        let headers = new Headers({
                            'Content-Type': 'application/json'
                        });

                        let options: RequestOptionsArgs = {
                            headers: headers
                        }

                        let response = await this.http.post(this.baseUrl + 'Authorization/Login', JSON.stringify(repr), options).toPromise();
                        let userRepr = (response.json() as UserRepresentation);
                        localStorage.setItem('token', userRepr.token);
                        return userRepr.token;
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