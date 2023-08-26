import { Component, HostListener, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductsPagesService } from 'src/app/services/products-pages.service';
import { Location } from '@angular/common';
import { InformatinTableProducts } from 'src/Model/informatin-table-products';
import { RatingApiServiceService } from 'src/app/services/rating-api-service.service';
import { CartService } from 'src/app/services/cart.service';
import { WishListService } from 'src/app/services/wish-list.service';
import { IProduct } from 'src/Model/i-product';
import { environment } from 'src/environments/environment.development';
import { ProductFilterService } from 'src/app/services/product-filter.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  AllProduct: IProduct[] = [];
  prodId:number=0;
  brands: string[] = [];
 @Input() product: IProduct| undefined ;
  informationTable: InformatinTableProducts[] = [];
  products: IProduct[] = [];
  discount: number = 14;
  public NoResults: boolean | undefined;
  localLang: string | null = localStorage.getItem('myLang');
  successMessage:string='';
  selectedsubCategoryId: number = 0;
  constructor(private productApiPages : ProductsPagesService ,
     private activatedRoute: ActivatedRoute , 
     private location :Location,
     private ratingApiService: RatingApiServiceService,
     private cartService:CartService,
     private wishListService : WishListService,
     private productFilterService: ProductFilterService
     ){}

  
  ngOnInit(): void {
    this.prodId = this.activatedRoute.snapshot.paramMap.has('productId')
        ? Number(this.activatedRoute.snapshot.paramMap.get('productId'))
        : 0;
  
      this.productApiPages.getproductsById(this.prodId).subscribe((prod) => {
        prod.discount = this.discount;
        prod.rating = 3;
        prod.image = environment.BaseApiUrl + prod.image;
        prod.name = this.localLang === 'ar' ? prod.productNameAR : prod.productNameEN;
        prod.description = this.localLang === 'ar' ? prod.descriptionAR : prod.descriptionEN;
        prod.brand = this.localLang === 'ar' ? prod.brandAR : prod.brandEN;
        this.selectedsubCategoryId = prod.subcategoryId;
        
        this.product = prod;
        console.log(this.product);
      });
  
      this.fetchProductDetails();
    }

  // Cart Button 

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

  // button go back
  
  goBack(): void {
    this.location.back();
  }

  // Rating 

    getColoredStars(rating: number): number[] {
      const filledStars = Math.floor(rating);
      const emptyStars = 5 - filledStars;
      return Array(filledStars).fill(1).concat(Array(emptyStars).fill(0));
    }

    // product details table 
    activeTab: string = 'details-table'; 



    isBold(str: string): boolean {
      return str.startsWith('<strong>');
    }
    
    // to remove tags from text json detailsTable
  removeTags(str: string): string {
    return str.replace(/<\/?[^>]+(>|$)/g, '');
  }

  // to fetch information Table

  fetchProductDetails() {
    this.productApiPages.getProductInformationTable().subscribe(
      (data) => {
        console.log(data); 
        this.informationTable = data;
      },
      (error) => {
        console.error('Failed to fetch product details:', error);
      }
    );
  }

// filter By subCategoryId 
subCategoryId: number | null = null;

  filterProductsBySubcategory() {
    if (this.subCategoryId !== null) {
      this.AllProduct = this.AllProduct.filter(prod => prod.subcategoryId === this.subCategoryId);
    }
  }


// Fixed div after scroll to show product

fixedDivVisible = false;
// fixedDivHeight = 'auto';

@HostListener('window:scroll', [])
onScroll() {
  this.fixedDivVisible = window.pageYOffset >= 800;
}

getFixedDivStyle() {
  return {
    display: this.fixedDivVisible ? '-webkit-box' : 'none',
  };
}






}
