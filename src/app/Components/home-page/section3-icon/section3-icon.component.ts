import { Component } from '@angular/core';
import 'bootstrap';
// import 'jquery/dist/jquery.min.js';

@Component({
  selector: 'app-section3-icon',
  templateUrl: './section3-icon.component.html',
  styleUrls: ['./section3-icon.component.css']
})
export class Section3IconComponent {
  localLang: string | null = localStorage.getItem("myLang");

}
