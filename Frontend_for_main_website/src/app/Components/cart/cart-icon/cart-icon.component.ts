import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IProduct } from 'src/Model/i-product';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-cart-icon',
  templateUrl: './cart-icon.component.html',
  styleUrls: ['./cart-icon.component.css']
})
export class CartIconComponent implements OnInit {
  totalOfOrder:number=0;
  cartItems: IProduct[] = [];
  constructor(private router: Router,private cartService: CartService) {}

  ngOnInit(): void {

    this.cartItems = this.cartService.getCartItemsWithQuantities();
    this.totalOfOrder = this.calculateTotalQuantities();

  }

  //function cartItems
  calculateTotalQuantities(): number {
    return this.cartItems.reduce((total, item) => total + item.quantity, 0);
  }

   // Cart Details
   isDropdownActive = false;

   showCartDetails(event: MouseEvent, element: HTMLDivElement): void {
     let target: HTMLElement = event.target as HTMLElement;
     // console.log(event.target);
     console.log(target);
     let cartDetails = element.querySelector('.dropdown-div');
     let detailsArrow = element.querySelector('.dropdown-arrow');
     if (
       (target.classList.contains('cart') ||
         target.classList.contains('items-num') ||
         target.classList.contains('fa-cart-plus')) &&
       !cartDetails?.classList.contains('active') &&
       !detailsArrow?.classList.contains('active')
     ) {
       cartDetails?.classList.add('active');
       detailsArrow?.classList.add('active');
     } else if (
       (target.classList.contains('cart') ||
         target.classList.contains('items-num') ||
         target.classList.contains('fa-cart-plus')) &&
       cartDetails?.classList.contains('active') &&
       detailsArrow?.classList.contains('active')
     ) {
       // console.log(element.classList.contains('cart'));
       cartDetails?.classList.remove('active');
       detailsArrow?.classList.remove('active');
     }
   }
}
