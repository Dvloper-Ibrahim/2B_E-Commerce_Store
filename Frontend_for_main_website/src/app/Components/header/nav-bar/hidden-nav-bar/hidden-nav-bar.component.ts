import { Component } from '@angular/core';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { ICategory } from 'src/Model/i-category';
import { IProduct } from 'src/Model/i-product';
import { ISubCategory } from 'src/Model/i-sub-category';
import { AuthService, DecodedJWT } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { CategoryService } from 'src/app/services/category.service';
import { ProductsPagesService } from 'src/app/services/products-pages.service';
import { SideMenuNavbarService } from 'src/app/services/side-menu-navbar.service';
import { SubCategoryService } from 'src/app/services/sub-category.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-hidden-nav-bar',
  templateUrl: './hidden-nav-bar.component.html',
  styleUrls: ['./hidden-nav-bar.component.css']
})
export class HiddenNavBarComponent {
  isDropdownOpen: boolean[] = [false, false, false, false, false,false];
 
  categories: ICategory[] = [];
  localLang: string | null = localStorage.getItem('myLang');
  cartItems: IProduct[] = [];
  token: string | null = localStorage.getItem('_2B_User');
  currentUser = {} as DecodedJWT | null;
  isAuthorized: boolean = false;

  constructor(private router: Router ,
    private productApiPages: ProductsPagesService,
    private sideMenuNavbarService:SideMenuNavbarService,
    private categorService: CategoryService,
    private subCatService: SubCategoryService,
    private cartService: CartService,
    private userService:UserService, private authService:AuthService) { }


  ngOnInit(): void {
    this.categorService.getAllCategories().subscribe((data) => {
      data.forEach((item) => {
        item.name = this.localLang == 'ar' ? item.nameAR : item.nameEN;
        item.description =
          this.localLang == 'ar' ? item.descriptionAR : item.descriptionEN;
      });
      this.categories = data;
    });
    this.isAuthorized = this.authService.isAuthenticated();
    this.currentUser = !!this.token ? jwtDecode(this.token) : null;
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


  toggleDropdown(index: number) {
    this.isDropdownOpen[index] = !this.isDropdownOpen[index];
  }

// for navigate from navbar to side bar in components

setActiveMenu(menu: string) {
  this.sideMenuNavbarService.setActiveMenu(menu);
}

//button in hidden navBar 

isNavBarVisible: boolean = true;
  isCartVisible: boolean = false;
  isAccountVisible:boolean=false;

  showNavBar() {
    this.isNavBarVisible = true;
    this.isCartVisible = false;
    this.isAccountVisible=false;
  }

  showCart() {
    this.isCartVisible = true;  
    this.isNavBarVisible = false;
    this.isAccountVisible=false;
  }

  showAccount(){
    this.isAccountVisible=true;
    this.isNavBarVisible = false;
    this.isCartVisible = false;
  }
  // language
  toggleLanguage(){
    this.localLang == "en" ?
    localStorage.setItem("myLang", "ar") :
    localStorage.setItem("myLang", "en");

    // console.log(localStorage.getItem("myLang"));
    location.reload();
  }

  // toggleMenu() {
  //   let menu = document.querySelector('ul.menu');
  //   menu?.classList.contains('active')
  //     ? menu?.classList.remove('active')
  //     : menu?.classList.add('active');
  // }

  isMenuOpen = false; 

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  signOut() {
    this.userService.signOut().subscribe({
      next: (res) => {
        localStorage.removeItem('_2B_User');
        alert(res.message);
        location.assign('');
      },
      error: (err) => alert('Something went wrong'),
    });
  }
}

