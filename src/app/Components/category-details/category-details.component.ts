import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter } from 'rxjs';
import { ICategory } from 'src/Model/i-category';
import { IProduct } from 'src/Model/i-product';
import { CategoryService } from 'src/app/services/category.service';
import { ProductsPagesService } from 'src/app/services/products-pages.service';
import { SortProductService } from 'src/app/services/sort-product.service';
import { SubCategoryService } from 'src/app/services/sub-category.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-category-details',
  templateUrl: './category-details.component.html',
  styleUrls: ['./category-details.component.css']
})
export class CategoryDetailsComponent implements OnInit {
  category = {} as ICategory;
  categoryID: number = 0;
  localLang: string | null = localStorage.getItem('myLang');
  products: IProduct[] = [];
  discount: number = 14;
  selectedBrands: string[] = [];
// FILTER PRICE
originalProducts: IProduct[] = []; 
minPrice: number =0;
maxPrice: number =0;
// FILTER BRAND
brands: string[] = [];
brandBox: { [brand: string]: { name: string, checked: boolean } } = {};
// PAGINATION
itemsPerPage: number = 16;
currentPage: number = 1;
totalPages: number = 0;
pages: number[] = [];
displayedProducts: IProduct[] = [];
selectedPage: number = 1;
  constructor(private categoryService: CategoryService, private route: ActivatedRoute,
    private subCatService: SubCategoryService, private prodService: ProductsPagesService,
    private sortProduct :SortProductService) {
    }

  ngOnInit(): void {
    if(this.categoryID != Number(this.route.snapshot.paramMap.get('categoryID'))){
    }

    this.categoryID = Number(this.route.snapshot.paramMap.get('categoryID'));
    this.categoryService.getCategoryById(this.categoryID).subscribe(data => {
      data.name = this.localLang == 'ar' ? data.nameAR : data.nameEN;
      this.category = data;
    })
    console.log(this.categoryID);
    

    this.subCatService.getSubCategoriesInCategory(this.categoryID).subscribe(data => {
      data.forEach(item => {
        item.name = this.localLang == 'ar' ? item.nameAR : item.nameEN;
      })
      this.category.subCategories = data;
    })

    this.prodService.getProductsByCategoryId(this.categoryID).subscribe(data => {
      data.forEach(prod => {
        prod.discount = this.discount
        prod.rating = 3
        prod.image = environment.BaseApiUrl +  prod.image;
        prod.name = this.localLang == "ar" ? prod.productNameAR : prod.productNameEN;
        prod.description = this.localLang == "ar" ? prod.descriptionAR : prod.descriptionEN;
        prod.brand = this.localLang == "ar" ? prod.brandAR : prod.brandEN;

        if(!this.brands.includes(prod.brand)) {
            this.brands.push(prod.brand);
          }
      })
      this.originalProducts = data;
      this.products = data;

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

    // Calculate total number of pages based on the total products and itemsPerPage
    this.totalPages = Math.ceil(this.products.length / this.itemsPerPage);

    // Initialize pages array with numbers from 1 to totalPages
    this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);
      this.updateDisplayedProducts();
    });
  }

    
  

 //-------------------------------------------------------------------------
       // SORT NUMBER OF PRODUCT

updateDisplayedProducts() {
  // Calculate the start index of the current page
  const startIndex = (this.currentPage - 1) * this.itemsPerPage;

  // Calculate the end index of the current page
  let endIndex = startIndex + this.itemsPerPage;

  // Check if the current page is the last page
  if (this.currentPage === this.totalPages) {
    // Adjust the end index to avoid exceeding the total number of products
    endIndex = this.products.length;
  }

  // Update the displayedProducts with the products for the current page
  this.displayedProducts = this.products.slice(startIndex, endIndex);
}


onItemsPerPageChange() {
  // Calculate total number of pages based on the new itemsPerPage value
  this.totalPages = Math.ceil(this.products.length / this.itemsPerPage);

  // Update pages array with numbers from 1 to totalPages
  this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);

  // Ensure that currentPage is within valid range
  this.currentPage = Math.min(Math.max(this.currentPage, 1), this.totalPages);

  // Update displayed products based on the current page and new itemsPerPage
  this.updateDisplayedProducts();
}

previousPage() {
  if (this.currentPage > 1) {
    this.currentPage--;
    this.updateDisplayedProducts();
  }
}

nextPage() {
  if (this.currentPage < this.totalPages) {
    this.currentPage++;
    this.updateDisplayedProducts();
  }
}

goToPage(page: number) {
  if (page >= 1 && page <= this.totalPages) {
    this.currentPage = page;
    this.updateDisplayedProducts();
  }
}



//--------------------------------------------------------------------------
    // SORT Button
    set sortingOption(value: string) {
      this.onSortingOptionChange(value)
    }
  
    onSortingOptionChange(val: string) {
      switch (val) {
        case 'nameAsc':
          this.sortProductsByNameAscending();
          break;
        case 'nameDesc':
          this.sortProductsByNameDescending();
          break;
        case 'priceHighToLow':
          this.sortProductsByPriceDescending();
          break;
        case 'priceLowToHigh':
          this.sortProductsByPriceAscending();
          break;
        default:
          this.showAllProducts(); // Show all products by default
          break;
      }
      this.updateDisplayedProducts();
      this.onItemsPerPageChange();
    }

    // sortProducts: IProduct[] = [];
  
    sortProductsByNameAscending() {
      this.products = this.sortProduct.sortByNameAscending(this.products);
    }
  
    sortProductsByNameDescending() {
      this.products = this.sortProduct.sortByNameDescending(this.products);
    }
  
    sortProductsByPriceAscending() {
      this.products = this.sortProduct.sortByPriceAscending(this.products);
    }
  
    sortProductsByPriceDescending() {
      this.products = this.sortProduct.sortByPriceDescending(this.products);
    }

    showAllProducts() {
      this.products = this.originalProducts; 
    }
//--------------------------------------------------------------------------
    // FILTER BY PRICE

applyFilters() {
  if (this.minPrice !== null && this.maxPrice !== null && this.minPrice >= 0 && this.maxPrice >= 0) {
    this.products = this.originalProducts.filter((product) => {
      const productPrice = product.price;
      return productPrice >= this.minPrice && productPrice <= this.maxPrice;
    });
  }
  this.updateDisplayedProducts();
  this.onItemsPerPageChange();
}


resetFilters() {
  this.minPrice = Math.min(...this.originalProducts.map((product) => product.price));
  this.maxPrice = Math.max(...this.originalProducts.map((product) => product.price));
  this.products = this.originalProducts;
  this.updateDisplayedProducts();
  this.onItemsPerPageChange();
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
    this.updateDisplayedProducts();
    this.onItemsPerPageChange();
  }
  
  resetBrandFilter() {
    // Reset the brand checkboxes
    this.brands.forEach(brand => {
      this.brandBox[brand].checked = false;
    });
  
    // Reset the product list to show all products
    this.products = this.originalProducts;
    this.updateDisplayedProducts();
    this.onItemsPerPageChange();
  }
  


}
