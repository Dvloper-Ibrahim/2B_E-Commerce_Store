import { Component } from '@angular/core';

@Component({
  selector: 'app-home-appliances-offers',
  templateUrl: './home-appliances-offers.component.html',
  styleUrls: ['./home-appliances-offers.component.css']
})
export class HomeAppliancesOffersComponent {
  localLang: string | null = localStorage.getItem("myLang");
}
