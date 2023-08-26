import { Injectable } from '@angular/core';
import { IProduct } from 'src/Model/i-product';
import { IProductsPages } from 'src/Model/i-products-pages';

@Injectable({
  providedIn: 'root'
})
export class WishListService {

  
  private wishItems: IProduct[] = [];

  constructor() { 
    const storedWishtItems = localStorage.getItem('wishItems');
    if (storedWishtItems) {
      this.wishItems = JSON.parse(storedWishtItems);
    }
  }

  addToWishList(item: IProduct) {
    this.wishItems.push(item);
    localStorage.setItem('wishItems', JSON.stringify(this.wishItems));
  }

  getWishListItems(): IProduct[] {
    return this.wishItems;
  }

  clearWishList() {
    this.wishItems = [];
    localStorage.removeItem('wishItems');
  }
}
