import { Injectable } from "@angular/core";
import { HubConnection, HubConnectionBuilder, LogLevel, JsonHubProtocol } from "@aspnet/signalr";

@Injectable()
export class RecomendationService {
    private _hubConnection: HubConnection;

    constructor() {
        this._hubConnection = new HubConnectionBuilder()         // TODO: inject dependecies
            .configureLogging(LogLevel.Trace)
            .withUrl("http://localhost:5000/recommendationHub")
            .withHubProtocol(new JsonHubProtocol())
            .build();

        

    }

    public ConnectionTest(cb: (message: string) => void){
        this._hubConnection.on("ReceiveMessage", (msg: string) => {
            console.log(msg);
             cb(msg);
            });
        let promise = this._hubConnection.start();
        promise.catch(err => console.error(err));
        promise.then(x => this._hubConnection.invoke("RecommendTest").catch(err => console.error(err)));
    }
}