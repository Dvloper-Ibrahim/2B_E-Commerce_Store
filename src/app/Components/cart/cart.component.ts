

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IProduct } from 'src/Model/i-product';
import { CartService } from 'src/app/services/cart.service';


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {


  cartItems: IProduct[] = [];
  total: number = 0;
  tax: number = 0;
  taxRate: number = 1; 
  quantity:number=1;
  constructor(private cartService: CartService,private router: Router) { }

  ngOnInit(): void {
    // this.cartItems = this.cartService.getCartItems();
    this.cartItems = this.cartService.getCartItemsWithQuantities();

    // this.cartItems.forEach(item => item.quantity = 1);
    this.calculateTotals();
  }

  getQuantity(item: IProduct): number {
    return item.quantity || 1; 
  }

  getSubtotal(item: IProduct): number {
    return item.price * this.getQuantity(item);
  }

  getSubtotalCart(): number {
    return this.cartItems.reduce((total, item) => total + (item.price * this.getQuantity(item)), 0);
  }

  getTax(): number {
    return this.getSubtotalCart() * 0; 
   }

  getTotal(): number {
    return this.cartItems.reduce((totalPrice, item) => totalPrice + this.getSubtotal(item), 0) + this.getTax();
  }

  private calculateTotals(): void {
    this.total = this.getTotal();
  }


  increaseQuantity(item: IProduct): void {
    item.quantity = this.getQuantity(item) + 1;
    this.calculateTotals();
  }
  

  decreaseQuantity(item: IProduct): void {
    const quantity = this.getQuantity(item);
    if (quantity > 1) {
      item.quantity = quantity - 1;
      this.calculateTotals();
    }
  }


  removeItemFromCart(item: IProduct): void {
    // Show confirmation dialog to the user
    const confirmDelete = confirm('Are you sure you want to remove this item from the cart?');
    if (confirmDelete) {
      const index = this.cartItems.indexOf(item);
      if (index !== -1) {
        this.cartItems.splice(index, 1);
        localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
        this.calculateTotals();
      }
    } else {
      // User canceled the delete operation
      console.log('Item removal was canceled');
    }
  }
  
  goToCheckout(){
    const total = this.getTotal();
    this.router.navigate(['/checkout'], { queryParams: { total: total } });
    }

}




