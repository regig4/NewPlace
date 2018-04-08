import { Component } from "@angular/core";
import { RecomendationsService } from "../../services/recomendations.service";


@Component({
    selector: 'recommended'
})
export class RecommendedComponent {
    public recomendations: string[];

    constructor(private service: RecomendationsService) {
       
    }
}