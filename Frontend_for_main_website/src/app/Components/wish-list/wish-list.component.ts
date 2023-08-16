import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IProduct } from 'src/Model/i-product';
import { IProductsPages } from 'src/Model/i-products-pages';
import { CartService } from 'src/app/services/cart.service';
import { ProductsPagesService } from 'src/app/services/products-pages.service';
import { WishListService } from 'src/app/services/wish-list.service';

@Component({
  selector: 'app-wish-list',
  templateUrl: './wish-list.component.html',
  styleUrls: ['./wish-list.component.css']
})
export class WishListComponent implements OnInit {
AllProduct: IProduct[] = [];
wishItems: IProduct[] = [];
successMessage:string='';

constructor(private productApiPages: ProductsPagesService , private router: Router ,private wishListService : WishListService) {}


ngOnInit(): void {
  this.wishItems = this.wishListService.getWishListItems();
  this.wishItems.forEach(item => item.quantity = 1);
}


// Cart Button 
addToCart(product: any) {
  try {
    this.wishListService.addToWishList(product);
    this.successMessage = `Added ${product.name} to cart successfully!`;
    alert(this.successMessage);
  } catch (error) {
    this.successMessage = `Failed to add ${product.name} to cart.`;
    alert(this.successMessage);
  }
}

removeItemFromWishlist(item: IProduct): void {
  // Show confirmation dialog to the user
  const confirmDelete = confirm('Are you sure you want to remove this item from the Wish list?');
  if (confirmDelete) {
    const index = this.wishItems.indexOf(item);
    if (index !== -1) {
      this.wishItems.splice(index, 1);
      localStorage.setItem('wishItems', JSON.stringify(this.wishItems));
    }
  } else {
    // User canceled the delete operation
    console.log('Item removal was canceled');
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

// decrease and increase quantity of product 

getQuantity(item: IProduct): number {
  return item.quantity || 1; 
}

increaseQuantity(item: IProduct): void {
  item.quantity = this.getQuantity(item) + 1;
}

decreaseQuantity(item: IProduct): void {
  const quantity = this.getQuantity(item);
  if (quantity > 1) {
    item.quantity = quantity - 1;
  }
}

}
