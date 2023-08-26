import { Component, HostListener } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css'],
})
export class FooterComponent {
  // distannceY: number = 250;
  scrollToTop() {
    window.scroll(0, 0);
  }
}
