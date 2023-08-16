import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IOrder } from 'src/Model/i-order';
import { IShipping } from 'src/Model/i-shipping';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-success-payment',
  templateUrl: './success-payment.component.html',
  styleUrls: ['./success-payment.component.css']
})
export class SuccessPaymentComponent implements OnInit {
  shipping = {} as IShipping;
  order = {} as IOrder;
  orderID: number = 0;
  constructor(
    private router: Router, private orderService: OrderService) {}

  ngOnInit(): void {
    this.shipping = JSON.parse(localStorage.getItem('order-shipping') || '');
    this.order = JSON.parse(localStorage.getItem('user-order') || '');
    this.orderService.placeOrder(this.order).subscribe({
      next: (data) => {
        this.orderID = data.id;
        this.shipping.orderId = data.id;
        localStorage.setItem('order-shipping', JSON.stringify(this.shipping));
        // console.log(data);
        localStorage.setItem('user-order', JSON.stringify(data));
      },
      error: (err) => console.log(err),
    });
    // this.shipping.orderId = this.order.id;
    // this.orderID = this.shipping.orderId;
  }

  async goBackToHome() {
    try {
      this.orderService.setShipping(this.shipping).subscribe({
        next: (data) => {
          // console.log(data);
        },
        error: (err) => console.log(err),
      });
      // const response = await this.orderService
      //   .setShipping(this.shipping)
      //   .toPromise();
      // console.log('API Response:', response);
      localStorage.removeItem('user-order');
      localStorage.removeItem('order-shipping');
      location.assign('/home');
      // this.router.navigate(['/home']);
    } catch (error) {
      console.error('API Error:', error);
    }
  }
}
