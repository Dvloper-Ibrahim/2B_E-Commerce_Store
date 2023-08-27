import { Component } from '@angular/core';
import 'bootstrap';
// import 'jquery/dist/jquery.min.js';


@Component({
  selector: 'app-section2-button',
  templateUrl: './section2-button.component.html',
  styleUrls: ['./section2-button.component.css']
})
export class Section2ButtonComponent {
  showDivAfter: {[key: string]: boolean} = {};

  toggleDiv(element: string) {
    this.showDivAfter = { divLeft: false, divMiddle: false, divRight: false };
    this.showDivAfter[element] = !this.showDivAfter[element];
  }
}
