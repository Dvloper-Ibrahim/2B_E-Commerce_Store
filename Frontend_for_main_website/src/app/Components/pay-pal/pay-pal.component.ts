import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment.development';
import { IPayPalConfig, ICreateOrderRequest } from 'ngx-paypal';
import { IOrder } from 'src/Model/i-order';
import { IShipping } from 'src/Model/i-shipping';


@Component({
  selector: 'app-pay-pal',
  templateUrl: './pay-pal.component.html',
  styleUrls: ['./pay-pal.component.css']
})
export class PayPalComponent implements OnInit {

  public payPalConfig?: IPayPalConfig;
  showSuccess!: any;
  cartTotal!: any;
  order = {} as IOrder;
  shipping = {} as IShipping;

  constructor(private router: Router) {}

  ngOnInit() {
    this.initConfig();
    this.cartTotal =
      JSON.parse(localStorage.getItem('cart_total') as any) || [];
    console.log(this.cartTotal);
    this.order = JSON.parse(localStorage.getItem('user-order') || '');
    this.shipping = JSON.parse(localStorage.getItem('order-shipping') || '');
  }

    // setOrderAndShipping() {
  // let date1 = new Date(),
  //   date2 = new Date();
  // date2.setDate(date2.getDate() + 3);
  // this.order.orderItems.forEach((oi) => {
  //   oi.paymentDate = date1;
  // });
  // this.order.orderDate = new Date();
  // this.order.arrivalDate = date2;
  // this.shipping.shippingMethod = '';
  // this.shipping.trackingNumber = '';
  // this.shipping.provider = '';
  // this.orderService.placeOrder(this.order).subscribe({
  //   next: (data) => {
  //     this.shipping.orderId = data.id;
  //     localStorage.setItem('order-shipping', JSON.stringify(this.shipping));
  //     console.log(data);
  //     // this.orderService.setShipping(this.shipping).subscribe((res) => {
  //     //   localStorage.setItem('order-shipping', JSON.stringify(res));
  //     // });
  //     localStorage.setItem('user-order', JSON.stringify(data));
  //   },
  //   error: (err) => console.log(err),
  // });
  // console.log(this.order);
  // console.log(this.shipping);
  // }

  private initConfig(): void {
    this.payPalConfig = {
      currency: 'EUR',
      clientId: `${environment.Client_ID}`,
      createOrderOnClient: (data) =>
        <ICreateOrderRequest>{
          intent: 'CAPTURE',
          purchase_units: [
            {
              amount: {
                currency_code: 'EUR',
                value: `${this.cartTotal}`,
                breakdown: {
                  item_total: {
                    currency_code: 'EUR',
                    value: `${this.cartTotal}`,
                  },
                },
              },
              items: [
                {
                  name: 'Enterprise Subscription',
                  quantity: '1',
                  category: 'DIGITAL_GOODS',
                  unit_amount: {
                    currency_code: 'EUR',
                    value: `${this.cartTotal}`,
                  },
                },
              ],
            },
          ],
        },
      advanced: {
        commit: 'true',
      },
      style: {
        label: 'paypal',
        layout: 'vertical',
      },
      onApprove: (data, actions) => {
        console.log(
          'onApprove - transaction was approved, but not authorized',
          data,
          actions
        );
        actions.order.get().then((details: any) => {
          console.log(
            'onApprove - you can get full order details inside onApprove: ',
            details
          );
        });
      },
      onClientAuthorization: (data) => {
        console.log(
          'onClientAuthorization - you should probably inform your server about completed transaction at this point',
          data
        );
        if (data.status === 'COMPLETED') {
          // this.setOrderAndShipping();
          // localStorage.removeItem('user-order');
          localStorage.removeItem('cart_total');
          localStorage.removeItem('__paypal_storage__');
          localStorage.removeItem('cartItems');
          // location.assign('/successCheckout');
          this.router.navigate(['/successCheckout']);
        }
        this.showSuccess = true;
      },
      onCancel: (data, actions) => {
        console.log('OnCancel', data, actions);
      },
      onError: (err) => {
        console.log('OnError', err);
      },
      onClick: (data, actions) => {
        console.log('onClick', data, actions);
      },
    };
  }

}
