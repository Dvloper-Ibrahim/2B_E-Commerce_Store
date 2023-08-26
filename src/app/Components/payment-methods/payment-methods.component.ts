import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IOrder } from 'src/Model/i-order';
import { IProduct } from 'src/Model/i-product';
import { IShipping } from 'src/Model/i-shipping';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-payment-methods',
  templateUrl: './payment-methods.component.html',
  styleUrls: ['./payment-methods.component.css']
})
export class PaymentMethodsComponent implements OnInit{

  localLang: string = "";
  cartItems: IProduct[] = [];
  total: number = 0;
  

  constructor(private cartService: CartService,private route: ActivatedRoute,private router: Router){}

  ngOnInit(): void {
    this.localLang = localStorage.getItem('myLang') || "en";
    this.cartItems = this.cartService.getCartItems();
    this.route.queryParams.subscribe((params) => {
      this.total = parseFloat(params['total'] || 0);
    });
  }

  backToShipping(){
    this.router.navigate(['/checkout']);
  }

  // when choose cash on delivery 

  paymentMethod: string = '';
  showDeliveryDiv: boolean = false; 
  showVodafoneCash: boolean = false; 
  showPaypal: boolean = false; 
  showCreditCard: boolean = false;

  toggleDeliveryDiv() {
    this.showDeliveryDiv = this.paymentMethod === 'cashOnDelivery';
    this.showVodafoneCash = false; 
    this.showPaypal=false;
    this.showCreditCard = false; 
  }

  toggleVodafone() {
    this.showVodafoneCash = this.paymentMethod === 'vodafone';
    this.showDeliveryDiv = false; 
    this.showPaypal=false;
    this.showCreditCard = false; 
  }

  togglePaypal(){
    this.showPaypal=this.paymentMethod==='paypal';
    this.showDeliveryDiv = false; 
    this.showVodafoneCash = false; 
    this.showCreditCard = false; 
  }

  paypal(){
    localStorage.setItem('cart_total', JSON.stringify(this.total));
    let myOrder: IOrder = JSON.parse(localStorage.getItem('user-order') || '');
    let myShipping: IShipping = JSON.parse(
      localStorage.getItem('order-shipping') || ''
    );
    // console.log(this.paymentMethod);
    let date1 = new Date(),
      date2 = new Date();
    date2.setDate(date2.getDate() + 3);
    myOrder.orderItems.forEach((oi) => {
      oi.paymentDate = date1;
    });
    myOrder.orderDate = new Date();
    myOrder.arrivalDate = date2;

    myOrder.paymentStatus = 'Paid';
    myOrder.trackingInformation = 'Order Placed';

    // myShipping.shippingMethod = '';
    // myShipping.trackingNumber = '';
    // myShipping.provider = '';

    localStorage.setItem('user-order', JSON.stringify(myOrder));
    localStorage.setItem('order-shipping', JSON.stringify(myShipping));
    // console.log(myOrder);
    // cashOnDelivery
    // paypal
    location.assign("/paymentMethod/paypal");
  }

  toggleCreditCard(){
    this.showCreditCard=this.paymentMethod === 'creditCard'
    this.showDeliveryDiv = false; 
    this.showVodafoneCash = false; 
    this.showPaypal=false;
  }
}
