import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { ProductsPagesService } from 'src/app/services/products-pages.service';
import { Router } from '@angular/router';
import 'bootstrap';
import 'jquery/dist/jquery.min.js';
import { IProductsPages } from 'src/Model/i-products-pages';
import { CartService } from 'src/app/services/cart.service';
import { WishListService } from 'src/app/services/wish-list.service';
import { IProduct } from 'src/Model/i-product';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-product-home',
  templateUrl: './product-home.component.html',
  styleUrls: ['./product-home.component.css'],
})
export class ProductHomeComponent implements OnInit {

  AllProduct: IProduct[] = [];
  products: IProduct[] = [];
  brands: string[] = [];
  selectedBrands: string[] = [];
  discount:number = 14;
  @Input() categoryId!: number; 
  @Input() subCategoryId!: number; 
  localLang: string | null = localStorage.getItem("myLang");

  constructor(private productApiPages: ProductsPagesService , 
    private router: Router, private cartService : CartService,
    private wishListService : WishListService){}

  ngOnInit(): void {
    this.productApiPages.getAllProduct().subscribe(data => {
      data.forEach(prod => {
        prod.discount = this.discount
        prod.rating = 3
        prod.image = environment.BaseApiUrl +  prod.image;
        prod.name = this.localLang == "ar" ? prod.productNameAR : prod.productNameEN
      })
      this.AllProduct = data;
    });

    if (this.categoryId) {
    this.productApiPages.getProductsByCategoryId(this.categoryId).subscribe(
      (data) => {
        data.forEach(prod => {
          prod.discount = this.discount
          prod.rating = 3
          prod.image = environment.BaseApiUrl +  prod.image;
          prod.name = this.localLang == "ar" ? prod.productNameAR : prod.productNameEN
        })

        this.products = data;
      },
      (error) => {
        console.log('There is erorr happened', error);
      }
    );
    }
    if (this.subCategoryId) {
    this.productApiPages.getProductsBySubCategoryId(this.subCategoryId).subscribe(
      (data) => {
        data.forEach(prod => {
          prod.discount = this.discount
          prod.rating = 3
          prod.image = environment.BaseApiUrl +  prod.image;
          prod.name = this.localLang == "ar" ? prod.productNameAR : prod.productNameEN
        })

        this.products = data;
      },
      (error) => {
        console.log('There is erorr happened', error);
      }
    );
    }
  }

// CART

  successMessage:string='';
  addToCart(product: any) {
    try {
      let newPrice: number | undefined = undefined;
  
      // if theres dicount
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

  
// ****************************************************

// handel scroll in card products

@ViewChild('cardContainer') cardContainer!: ElementRef;


handleCardClick(event: MouseEvent, product: any, isLeftClick: boolean = true) {
  // implement the order on click card 
  console.log('Clicked on card:', product);

  const cardElement = (event.target as HTMLElement).closest('.card');
  if (!cardElement) return; // to confirm the element is exist

  const containerElement = this.cardContainer.nativeElement;
  const cardWidth = cardElement.clientWidth;

  const cardOffsetLeft = cardElement.getBoundingClientRect().left;
  const containerScrollLeft = containerElement.scrollLeft;
  const containerWidth = containerElement.clientWidth;

  const scrollAmount = containerWidth / 2;

  if (isLeftClick) {
    const scrollLeft = containerScrollLeft + cardOffsetLeft - scrollAmount;

    containerElement.scrollTo({
      left: scrollLeft,
      behavior: 'smooth',
    });
  } else {
    const scrollRight = containerScrollLeft + cardOffsetLeft + cardWidth - scrollAmount;

    containerElement.scrollTo({
      left: scrollRight,
      behavior: 'smooth',
    });
  }
}

handleCardLeftClick(event: MouseEvent, product: any) {
  this.handleCardClick(event, product);
}

handleCardRightClick(event: MouseEvent, product: any) {
  event.preventDefault();
  this.handleCardClick(event, product, false);
}



toggleBrandMenu() {
  this.showBrandMenu = !this.showBrandMenu;
}


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









