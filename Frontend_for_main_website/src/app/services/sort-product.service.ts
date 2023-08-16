import { EventEmitter, Injectable, Output } from '@angular/core';
import { IProduct } from 'src/Model/i-product';

@Injectable({
  providedIn: 'root'
})
export class SortProductService {

  constructor() { }

     //************ SORT PRODUCTS 


       // SORT PRODUCTS 
       sortByNameAscending(products: IProduct[]): IProduct[] {
        return products.slice().sort((a, b) => a.name.localeCompare(b.name));
      }
    
      sortByNameDescending(products: IProduct[]): IProduct[] {
        return products.slice().sort((a, b) => b.name.localeCompare(a.name));
      }
    
      sortByPriceAscending(products: IProduct[]): IProduct[] {
        return products.slice().sort((a, b) => a.price - b.price);
      }
    
      sortByPriceDescending(products: IProduct[]): IProduct[] {
        return products.slice().sort((a, b) => b.price - a.price);
      }
}
