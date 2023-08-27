import {
  Component,
  ElementRef,
  HostListener,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';

import { ProductsPagesService } from 'src/app/services/products-pages.service';
import 'bootstrap';
// import 'jquery/dist/jquery.min.js';
// import { IProductsPages } from 'src/Model/i-products-pages';
// import { SideMenuNavbarService } from 'src/app/services/side-menu-navbar.service';
import { ICategory } from 'src/Model/i-category';
import { CategoryService } from 'src/app/services/category.service';
import { SubCategoryService } from 'src/app/services/sub-category.service';
import { ISubCategory } from 'src/Model/i-sub-category';
import { CartService } from 'src/app/services/cart.service';
import { IProduct } from 'src/Model/i-product';

let navInSmallScreenAfterScroll: HTMLElement | null;

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css'],
})
export class NavBarComponent implements OnInit {
  // @ViewChild('mobileTabletNavItem') mobileTabletNavItem!: ElementRef;
  categories: ICategory[] = [];
  localLang: string | null = localStorage.getItem('myLang');

  // *********************************************
  //  filter

  // ********************************************
  isNavbarFixed = false;
  cartItems: IProduct[] = [];
  distanncY = 250;
  searchResults: string[] = [];
  searchQuery: string = '';

  constructor(
    private router: Router,
    private categorService: CategoryService,
    private subCatService: SubCategoryService,
    private productApiPages: ProductsPagesService) {}

  ngOnInit(): void {
    this.categorService.getAllCategories().subscribe((data) => {
      data.forEach((item) => {
        item.name = this.localLang == 'ar' ? item.nameAR : item.nameEN;
        item.description =
          this.localLang == 'ar' ? item.descriptionAR : item.descriptionEN;
      });
      this.categories = data;
    });
  }



  getSubCatsNum(catId: number): number {
    let num: number = 0;
    this.subCatService.getSubCategoriesInCategory(catId).subscribe((data) => {
      num = data.length;
    });
    return num;
  }

  getSubSubCatsNum(subCatId: number): number {
    let num: number = 0;
    this.subCatService
      .getSubCategoriesInSubCategory(subCatId)
      .subscribe((data) => {
        num = data.length;
      });
    return num;
  }

  getSubCategories(catId: number): ISubCategory[] {
    let subCategories: ISubCategory[] = [];
    this.subCatService.getSubCategoriesInCategory(catId).subscribe((data) => {
      data.forEach((item) => {
        item.name = this.localLang == 'ar' ? item.nameAR : item.nameEN;
      });
      subCategories = data;
    });
    return subCategories;
  }

  getSubSubCategories(subCatId: number): ISubCategory[] {
    let subSubCategories: ISubCategory[] = [];
    this.subCatService
      .getSubCategoriesInSubCategory(subCatId)
      .subscribe((data) => {
        data.forEach((item) => {
          item.name = this.localLang == 'ar' ? item.nameAR : item.nameEN;
        });
        subSubCategories = data;
      });
    return subSubCategories;
  }


  @HostListener('window:scroll', ['$event'])
  onWindowScroll(event: any) {
    if (window.scrollY > this.distanncY) {
      this.isNavbarFixed = true;
      document.querySelector('.navbar')?.classList.add('fixed');
    } else {
      this.isNavbarFixed = false;
      document.querySelector('.navbar')?.classList.remove('fixed');
    };



  }

  // to show side bar in hidden bar
  showSideBar = false;

  toggleSideBar() {
    this.showSideBar = !this.showSideBar;
  }
  // to close side bar from any where
  closeSideBar() {
    this.showSideBar = false;
  }

  // to close element of side bar on click
  isItemOpen = false;

  toggleItem() {
    this.isItemOpen = !this.isItemOpen;
  }

  // to show and hidden search input in small screen
  isSearchActive: boolean = false;

  toggleSearch() {
    this.isSearchActive = !this.isSearchActive;
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    const cartIcon = document.querySelector('.cart');
    const cartDetails = document.querySelector('.dropdown-div');
    const detailsArrow = document.querySelector('.dropdown-arrow');

    if (cartIcon && !cartIcon.contains(event.target as HTMLElement)) {
      cartDetails?.classList.remove('active');
      detailsArrow?.classList.remove('active');
    }
  }


  refreshPage() {
    location.reload();
  }

  setSearch(e: Event) {
    this.searchResults = [];
    let input = e.target as HTMLInputElement;
    this.productApiPages.searchFor(input.value)?.subscribe({
      next: data => {
        data.forEach(item => {
          item.name = this.localLang == 'ar' ? item.productNameAR : item.descriptionEN
          this.searchResults.push(item.name.slice(0,30) + " ...");
          this.searchResults = this.searchResults.slice(0, 5)
        })
      },
      error:err => console.log(err)
    })
  }

  goToSearch(eve: Event, result: string) {
    let resultElement = eve.target as HTMLElement;
    let input = resultElement.parentElement
    ?.previousElementSibling as HTMLInputElement;
    result = result.slice(0, -4);
    // console.log(result);
    input.value = result;
    this.searchQuery = result;
    this.searchResults = [];
  // console.log(this.searchQuery);
  if (this.searchQuery.trim() !== '') {
    this.searchResults = [];
    console.log(this.searchQuery);
    this.router.navigate(['home/searchResult'], {
        queryParams: { query: this.searchQuery },
      });
      // location.assign(`/home/searchResult?query=${this.searchQuery}`)
    }
  }

}
