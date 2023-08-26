import { Component, OnInit } from '@angular/core';
import { ICheckout } from 'src/Model/i-checkout';
import { IProduct } from 'src/Model/i-product';
import { CartService } from 'src/app/services/cart.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IOrder } from 'src/Model/i-order';
import { IOrderItem } from 'src/Model/i-order-item';
import { DecodedJWT } from 'src/app/services/auth.service';
import jwtDecode from 'jwt-decode';
import { IShipping } from 'src/Model/i-shipping';

@Component({
  selector: 'app-check-out',
  templateUrl: './check-out.component.html',
  styleUrls: ['./check-out.component.css']
})
export class CheckOutComponent implements OnInit {

  localLang: string = '';
  cartItems: IProduct[] = [];

  order: IOrder = {} as IOrder;
  oredrItems: IOrderItem[] = [];
  token: string | null = localStorage.getItem('_2B_User');
  currentUser: DecodedJWT | null = !!this.token ? jwtDecode(this.token) : null;

  total: number = 0;
  shipping: IShipping = {} as IShipping;
  
  constructor(private cartService: CartService,private route: ActivatedRoute,private router: Router){}

  ngOnInit(): void {
    this.localLang = localStorage.getItem('myLang') || "en";
    this.cartItems = this.cartService.getCartItems();

    // let dateNow = new Date();
    this.cartItems.forEach((item) => {
      let orderItem: IOrderItem = {} as IOrderItem;
      orderItem.quantity = item.quantity;
      orderItem.amount = item.price;
      // orderItem.paymentDate = dateNow;
      orderItem.productId = item.id;
      this.oredrItems.push(orderItem);
    });
    this.order.orderItems = this.oredrItems;

    this.route.queryParams.subscribe((params) => {
      this.total = parseFloat(params['total'] || 0);
      // this.order.orderDate = dateNow;
      this.order.totalAmount = this.total;
      this.order.userId = this.currentUser?.nameid as string;
      // console.log(this.order);
      localStorage.setItem('user-order', JSON.stringify(this.order));
    });
  }

  goToPaymentMethods(){
    // console.log(this.shipping);
    this.shipping.cost = this.total;
    localStorage.setItem('order-shipping', JSON.stringify(this.shipping));
    this.router.navigate(['/checkout/payment']);
    this.router.navigate(['/checkout/payment'], { queryParams: { total: this.total } });
  }

}
