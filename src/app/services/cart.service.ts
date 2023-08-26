import { Injectable } from '@angular/core';
import { IProduct } from 'src/Model/i-product';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private cartItems: IProduct[] = [];

  constructor() { 
    const storedCartItems = localStorage.getItem('cartItems');
    if (storedCartItems) {
      this.cartItems = JSON.parse(storedCartItems);
    }
  }

  // addToCart(item: IProduct) {
  //   this.cartItems.push(item);
  //   localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
  // }

  addToCart(item: IProduct, newPrice?: number) {
    // إذا تم تمرير السعر المخفض (newPrice)، استخدمه، وإلا استخدم السعر الأصلي
    const price = newPrice !== undefined ? newPrice : item.price;

    // قم بإنشاء نسخة من المنتج مع السعر المناسب
    const cartItem: IProduct = {
      ...item,
      price: price
    };

    this.cartItems.push(cartItem);
    localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
  }


  getCartItemsWithQuantities(): IProduct[] {
    const productMap = new Map<number, IProduct>();
  
    this.cartItems.forEach(item => {
      if (productMap.has(item.id)) {
        const existingItem = productMap.get(item.id);
        if (existingItem) {
          existingItem.quantity++;
        }
      } else {
        const newItem = { ...item, quantity: 1 };
        productMap.set(item.id, newItem);
      }
    });
  
    return Array.from(productMap.values());
  }
  
  
  

  
  getCartItems(): IProduct[] {
    return this.cartItems;
  }

  clearCart() {
    this.cartItems = [];
    localStorage.removeItem('cartItems');
  }
}
