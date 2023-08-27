import { Component } from '@angular/core';
import 'bootstrap';
// import 'jquery/dist/jquery.min.js';
@Component({
  selector: 'app-carousel-home1',
  templateUrl: './carousel-home1.component.html',
  styleUrls: ['./carousel-home1.component.css']
})
export class CarouselHome1Component {
  localLang: string | null = localStorage.getItem("myLang");

}
