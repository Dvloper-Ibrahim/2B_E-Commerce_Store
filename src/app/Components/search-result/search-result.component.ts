import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from 'src/Model/i-product';
import { IProductsPages } from 'src/Model/i-products-pages';
import { CartService } from 'src/app/services/cart.service';
import { ProductsPagesService } from 'src/app/services/products-pages.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.css']
})
export class SearchResultComponent implements OnInit {
  // @Input() searchResults: IProductsPages[] = [];
  localLang: string = '';
  searchedProds: IProduct[] = [];
  searchQuery: string = ''; // The variable that will hold the search value
  discount:number = 14;
  products: IProduct[] = [];
// FILTER PRICE
originalProducts: IProduct[] = []; 
minPrice: number =0;
maxPrice: number =0;
// FILTER BRAND
brands: string[] = [];
brandBox: { [brand: string]: { name: string, checked: boolean } } = {};

  constructor(private productApiPages: ProductsPagesService , private router: Router ,private cartService : CartService,private route: ActivatedRoute) {} 
  ngOnInit(): void {
    this.localLang = localStorage.getItem('myLang') || 'en';
    // Extract the search value from the parameters sent via the link
    this.route.queryParams.subscribe((params) => {
      this.searchQuery = params['query'] || '';
      if (this.searchQuery.trim() !== '') {
        this.search();
      }
       });
      // Move the following lines here
  this.productApiPages.searchFor(this.searchQuery)?.subscribe((data) => {
    data.forEach(prod => {
      prod.discount = this.discount
      prod.rating = 3
      prod.image = environment.BaseApiUrl +  prod.image;
      prod.name = this.localLang == "ar" ? prod.productNameAR : prod.productNameEN
      prod.description = this.localLang == "ar" ? prod.descriptionAR : prod.descriptionEN
    });
    this.searchedProds = data;

    // Calculate the minimum and maximum prices from the products array
    const minProductPrice = Math.min(...data.map((product) => product.price));
    const maxProductPrice = Math.max(...data.map((product) => product.price));

    // Set the initial values for minPrice and maxPrice
    this.minPrice = minProductPrice;
    this.maxPrice = maxProductPrice;

    // Initialize brandBox with all brands and set their checked status to false
    this.brands.forEach(brand => {
      this.brandBox[brand] = { name: brand, checked: false };
    });
  });

 
  }

  search(): void {
    //  Call the search function based on the entered value
    this.productApiPages.searchFor(this.searchQuery)?.subscribe((data) => {
      data.forEach(prod => {
        prod.discount = this.discount
        prod.rating = 3
        prod.image = environment.BaseApiUrl +  prod.image;
        prod.name = this.localLang == "ar" ? prod.productNameAR : prod.productNameEN
        prod.description = this.localLang == "ar" ? prod.descriptionAR : prod.descriptionEN
      })
      this.searchedProds = data;
    });
  }

   // Cart Button 
   successMessage:string = '';
   
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

     // FILTER BY PRICE

applyFilters() {
  if (this.minPrice !== null && this.maxPrice !== null && this.minPrice >= 0 && this.maxPrice >= 0) {
    this.products = this.originalProducts.filter((product) => {
      const productPrice = product.price;
      return productPrice >= this.minPrice && productPrice <= this.maxPrice;
    });
  }

}


resetFilters() {
  this.minPrice = Math.min(...this.originalProducts.map((product) => product.price));
  this.maxPrice = Math.max(...this.originalProducts.map((product) => product.price));
  this.products = this.originalProducts;

}

//--------------------------------------------------------------------------
  // FILTER BY BRAND
  
  applyBrandFilter() {
    const selectedBrands = this.brands.filter(brand => this.brandBox[brand].checked);
    if (selectedBrands.length === 0) {
      // If no brands are selected, reset the filter to show all products
      this.products = this.originalProducts;
    } else {
      // Filter products based on selected brands
      this.products = this.originalProducts.filter(product => selectedBrands.includes(product.brand));
    }

  }
  
  resetBrandFilter() {
    // Reset the brand checkboxes
    this.brands.forEach(brand => {
      this.brandBox[brand].checked = false;
    });
  
    // Reset the product list to show all products
    this.products = this.originalProducts;
  }
  
}
