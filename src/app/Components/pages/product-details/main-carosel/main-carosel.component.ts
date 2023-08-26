import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { ProductsPagesService } from 'src/app/services/products-pages.service';
import {  Router } from '@angular/router';
import { ProductFilterService } from 'src/app/services/product-filter.service';
import 'bootstrap';
import 'jquery/dist/jquery.min.js';
import { IProduct } from 'src/Model/i-product';
import { environment } from 'src/environments/environment.development';
@Component({
  selector: 'app-main-carosel',
  templateUrl: './main-carosel.component.html',
  styleUrls: ['./main-carosel.component.css']
})
export class MainCaroselComponent implements OnInit {

  @Input() AllProduct: IProduct[] = []; 
  product: any;
  brands: string[] = [];
  selectedBrands: string[] = [];
// ***********************************

carouselItems: IProduct[] = [];
carouselItemsChunks: IProduct[][] = [];
currentSlide = 0;
localLang: string | null = localStorage.getItem('myLang');

// ===================================

// @Input() selectedsubCategoryId: number= 0;// Receive the selected category ID from the parent component

  constructor(private productApiPages: ProductsPagesService , private router: Router , private productFilterService :ProductFilterService ){}

  ngOnInit(): void {
    this.productApiPages.getAllProduct().subscribe(data => {
      data.forEach(prod => {
        prod.rating = 3
        prod.image = environment.BaseApiUrl +  prod.image;
        prod.name = this.localLang == "ar" ? prod.productNameAR : prod.productNameEN
      })
      this.AllProduct = data;
      this.chunkCarouselItems(); 
      this.setCurrentSlide(0); 
    });

    // this.productFilterService.filterBySubCategoryId(this.AllProduct, this.selectedsubCategoryId);

  }

  @Input() set selectedsubCategoryId(id: number){
    this.AllProduct = this.AllProduct.filter(p => p.subcategoryId == id);
    this.chunkCarouselItems()
  }
  
  
  addToCart(product: any) {
    console.log('Added to cart:', product);
  }
 

  
// ****************************************************


chunkCarouselItems() {
  const chunkSize = 4;
  const startIndex = this.currentSlide * chunkSize;
  this.carouselItemsChunks = [this.AllProduct.slice(startIndex, startIndex + chunkSize)];
}




 // product carosel
 setCurrentSlide(index: number) {
  this.currentSlide = index;
  this.chunkCarouselItems();
}
 



// *******************************************

toggleBrandMenu() {
  this.showBrandMenu = !this.showBrandMenu;
}

onBrandCheckboxChange() {
  this.productApiPages.getAllProduct().subscribe(data => {
    this.AllProduct = data.filter(prod => {
      if (this.selectedBrands.length === 0) {
        return true;
      }
      return this.selectedBrands.includes(prod.brand?.trim() || '');
    });
  });
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

prodId=0;
  NavigateToprodDetails(product: IProduct) {
    this.productApiPages.getproductsById(product.id)
      .subscribe({
        next: (data: IProduct) => {
          this.product = data;
          console.log(this.product);

          this.prodId = product.id;

          this.router.navigate(['/product', product.id])
                    .then(() => {
                      // refresh the page after click new product 
                      window.location.reload();
                    })
        },
        error: (error: any) => {
          console.error('Failed to load product details', error);
        }
      });
  }

  // Filter products based on subCategoryId


  // ngOnChanges(changes: SimpleChanges): void {
  //   if ('selectedsubCategoryId' in changes) {
  //     // Call the filter method when the selected category changes
  //     this.filterProductsBySubcategory();
  //   }
  // }




  filterProductsBySubcategory() {
    // const subCategoryId: number = this.selectedsubCategoryId !== null ? this.selectedsubCategoryId : 0;
    // this.AllProduct = this.productFilterService.filterBySubCategoryId(this.AllProduct, subCategoryId);
    // this.chunkCarouselItems();
  }
}
