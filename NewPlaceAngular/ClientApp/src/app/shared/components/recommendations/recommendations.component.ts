import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-recommendations',
  templateUrl: './recommendations.component.html',
  styleUrls: ['./recommendations.component.css']
})
export class RecommendationsComponent implements OnInit {

  latitude = 50.049683;
  longitude = 19.944544;
  mapType = 'satellite';

  markers = [
    {
      lat: 50.049683,
      lng: 19.944544,
      id: 1
    },
    {
      lat: 50.049683,
      lng: 19.541544,
      id: 2
    }
  ]

  constructor(private router: Router) { }

  ngOnInit() {
  }


}
