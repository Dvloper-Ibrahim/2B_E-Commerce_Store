
import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { IProduct } from 'src/Model/i-product';
import { IProductsPages } from 'src/Model/i-products-pages';
import { CartService } from 'src/app/services/cart.service';
import { ProductsPagesService } from 'src/app/services/products-pages.service';
import { WishListService } from 'src/app/services/wish-list.service';
import { environment } from 'src/environments/environment.development';

// interface ComputerComponentInputs {
//   category: number;
//   subCategory: number;
//   subSubCategory: number;
// }

@Component({
  selector: 'app-products-pages',
  templateUrl: './products-pages.component.html',
  styleUrls: ['./products-pages.component.css']
})

export class ProductsPagesComponent implements OnInit, OnChanges {
  

  AllProduct: IProduct[] = [];
  @Input() products: IProduct[] = [];
  brands: string[] = [];
  selectedBrands: string[] = [];


  successMessage:string='';

  constructor(private productApiPages: ProductsPagesService , private router: Router ,
    private cartService : CartService,private wishListService : WishListService) {}

  ngOnInit(): void {
    this.productApiPages.getAllProduct().subscribe(data => {
      data.forEach(prod => {
        prod.rating = 3
        prod.image = environment.BaseApiUrl +  prod.image;
        // prod.name = this.localLang == "ar" ? prod.productNameAR : prod.productNameEN
      })
      this.AllProduct = data;
      // this.updateProductsList();
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['computerComponentInputs']) {
    }
  }




  // Cart Button 
  addToCart(product: any) {
    try {
      this.cartService.addToCart(product);
      this.successMessage = `Added ${product.name} to cart successfully!`;
      alert(this.successMessage);
    } catch (error) {
      this.successMessage = `Failed to add ${product.name} to cart.`;
      alert(this.successMessage);
    }
  }

  // Wish List 

  addToWishList(product: any) {
    try {
      this.wishListService.addToWishList(product);
      this.successMessage = `Added ${product.name} to Wish List successfully!`;
      alert(this.successMessage);
    } catch (error) {
      this.successMessage = `Failed to add ${product.name} to Wish List.`;
      alert(this.successMessage);
    }
  }
  
  toggleBrandMenu() {
    this.showBrandMenu = !this.showBrandMenu;
  }



  // Other functions related to brand filters and UI interactions...

  showBrandMenu: boolean = false;


  // Rating 

  getColoredStars(rating: number): number[] {
    const filledStars = Math.floor(rating);
    const emptyStars = 5 - filledStars;
    return Array(filledStars).fill(1).concat(Array(emptyStars).fill(0));
  }

  // product details

  prodDetails(prodId:number){
    this.router.navigate(['/product',prodId]);
 }
  

 

}
