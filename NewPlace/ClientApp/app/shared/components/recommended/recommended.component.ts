import { Component } from "@angular/core";
import { RecomendationService } from "../../services/recomendations.service";


@Component({
    selector: 'recommended'
})
export class RecommendedComponent {
    public recomendations: string[] | undefined;

    constructor(private service: RecomendationService) {
       service.ConnectionTest(() => console.log("ok"));
    }
}