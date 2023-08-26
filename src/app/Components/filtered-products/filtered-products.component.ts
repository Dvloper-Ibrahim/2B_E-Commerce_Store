import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { IProduct } from 'src/Model/i-product';
import { CartService } from 'src/app/services/cart.service';
import { WishListService } from 'src/app/services/wish-list.service';

@Component({
  selector: 'app-filtered-products',
  templateUrl: './filtered-products.component.html',
  styleUrls: ['./filtered-products.component.css']
})
export class FilteredProductsComponent implements OnChanges {
  constructor(private cartService : CartService,
    private wishListService : WishListService, private router: Router) {}

  filteredProducts: IProduct[] = [];

  products: IProduct[] = [];
  @Input() set currentProds(prods: IProduct[]){
    this.products = prods;
  }

  @Input() minPrice: number = 0;
  @Input() maxPrice: number = 0;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['products'] || changes['minPrice'] || changes['maxPrice']) {
      this.applyFilters();
    }
  }

  // ... FILTER BY PRICE 

  applyFilters() {
    if (this.minPrice !== null && this.maxPrice !== null) {
      this.filteredProducts = this.products.filter((product) => {
        const productPrice = product.price;
        return productPrice >= this.minPrice && productPrice <= this.maxPrice;
      });
    } else {
      // If no minPrice and maxPrice are set, show all products
      this.filteredProducts = this.products;
    }
  }


//-----------------------------------------------------
  successMessage:string='';

  // addToCart(product: any) {
  //   try {
  //     this.cartService.addToCart(product);
  //     this.successMessage = `Added ${product.name} to cart successfully!`;
  //     alert(this.successMessage);
  //   } catch (error) {
  //     this.successMessage = `Failed to add ${product.name} to cart.`;
  //     alert(this.successMessage);
  //   }
  // }

  //CART
  addToCart(product: any) {
    try {
      let newPrice: number | undefined = undefined;
  
      // If theres discount
      if (product.discount) {
        newPrice = product.price * ((100 - product.discount) * 0.01);
      }
  
      this.cartService.addToCart(product, newPrice);
      this.successMessage = `Added ${product.name} to cart successfully!`;
      alert(this.successMessage);
    } catch (error) {
      this.successMessage = `Failed to add ${product.name} to cart.`;
      alert(this.successMessage);
    }
  }

  

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
