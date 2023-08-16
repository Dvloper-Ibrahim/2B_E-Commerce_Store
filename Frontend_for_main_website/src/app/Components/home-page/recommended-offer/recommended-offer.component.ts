import { Component } from '@angular/core';

@Component({
  selector: 'app-recommended-offer',
  templateUrl: './recommended-offer.component.html',
  styleUrls: ['./recommended-offer.component.css']
})
export class RecommendedOfferComponent {
  localLang: string | null = localStorage.getItem("myLang");
}
