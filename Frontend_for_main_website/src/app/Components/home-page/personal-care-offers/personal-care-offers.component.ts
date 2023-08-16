import { Component } from '@angular/core';

@Component({
  selector: 'app-personal-care-offers',
  templateUrl: './personal-care-offers.component.html',
  styleUrls: ['./personal-care-offers.component.css']
})
export class PersonalCareOffersComponent {
  localLang: string | null = localStorage.getItem("myLang");

}
