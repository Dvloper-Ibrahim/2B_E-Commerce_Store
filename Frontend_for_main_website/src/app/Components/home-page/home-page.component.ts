import { Component, OnInit } from '@angular/core';
import { interval } from 'rxjs';
// import 'bootstrap';
// import 'jquery/dist/jquery.min.js';
@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent implements OnInit {
  //Date
  date: Date = new Date();

  ngOnInit(): void {
    // for update date
    interval(1000).subscribe(() => {
      this.date = new Date();
    });
  }
}
