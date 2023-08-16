import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ProductsPagesService } from 'src/app/services/products-pages.service';
import { ProductFilterService } from 'src/app/services/product-filter.service';
import { IProduct } from 'src/Model/i-product';
import { CartService } from 'src/app/services/cart.service';
import { environment } from 'src/environments/environment.development';
import { Router } from '@angular/router';
import { ISubCategory } from 'src/Model/i-sub-category';

@Component({
  selector: 'app-side-carosel',
  templateUrl: './side-carosel.component.html',
  styleUrls: ['./side-carosel.component.css']
})
export class SideCaroselComponent implements OnInit, OnChanges {

  @Input() selectedsubCategoryId: number = 0;
  AllProduct: IProduct[] = [];
  carouselItems: IProduct[] = [];
  carouselItemsChunks: IProduct[][] = [];
  currentSlide = 0;
  localLang: string | null = localStorage.getItem('myLang');
  // selectedSubCategoryId: number = 0;
  product: any;

  constructor(
    private productApiPages: ProductsPagesService,
    private productFilterService: ProductFilterService,
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productApiPages.getAllProduct().subscribe(data => {
      data.forEach(prod => {
        prod.rating = 3
        prod.image = environment.BaseApiUrl + prod.image;
        prod.name = this.localLang == "ar" ? prod.productNameAR : prod.productNameEN
      });
      this.AllProduct = data;
      this.filterProductsBySubcategory(); 
    });
  }

  successMessage: string = '';

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

  chunkCarouselItems() {
    const chunkSize = 2;
    const startIndex = this.currentSlide * chunkSize;
    this.carouselItemsChunks = [this.AllProduct.slice(startIndex, startIndex + chunkSize)];
  }

  setCurrentSlide(index: number) {
    this.currentSlide = index;
    this.chunkCarouselItems();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if ('selectedsubCategoryId' in changes) {
      this.filterProductsBySubcategory();
    }
  }

  filterProductsBySubcategory() {
    this.carouselItems = this.AllProduct.filter(prod => prod.subcategoryId === this.selectedsubCategoryId);
    this.chunkCarouselItems();
  }

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

}
