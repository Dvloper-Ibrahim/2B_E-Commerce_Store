import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductsPagesService } from 'src/app/services/products-pages.service';
import 'bootstrap';
import 'jquery/dist/jquery.min.js';
import { CartService } from 'src/app/services/cart.service';
import { IProduct } from 'src/Model/i-product';
import { UserService } from 'src/app/services/user.service';
import { AuthService, DecodedJWT } from 'src/app/services/auth.service';
import jwtDecode from 'jwt-decode';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  localLang: string = "";
  token: string | null = localStorage.getItem('_2B_User');
  currentUser = {} as DecodedJWT | null;
  isAuthorized: boolean = false;

  showCartDetails(event: MouseEvent, element: HTMLDivElement): void {
    let target: HTMLElement = event.target as HTMLElement;

    // console.log(event.target);
    console.log(target);
    let cartDetails = element.querySelector('.dropdown-div');
    let detailsArrow = element.querySelector('.dropdown-arrow');
    if (
      (target.classList.contains('cart') ||
        target.classList.contains('items-num') ||
        target.classList.contains('fa-cart-plus')) &&
      !cartDetails?.classList.contains('active') &&
      !detailsArrow?.classList.contains('active')
    ) {
      cartDetails?.classList.add('active');
      detailsArrow?.classList.add('active');
    } else if (
      (target.classList.contains('cart') ||
        target.classList.contains('items-num') ||
        target.classList.contains('fa-cart-plus')) &&
      cartDetails?.classList.contains('active') &&
      detailsArrow?.classList.contains('active')
    ) {
      // console.log(element.classList.contains('cart'));
      cartDetails?.classList.remove('active');
      detailsArrow?.classList.remove('active');
    }
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    const cartDetails = document.querySelector('.dropdown-div');
    const detailsArrow = document.querySelector('.dropdown-arrow');
    
    if (cartDetails?.classList.contains('active') && detailsArrow?.classList.contains('active')) {
      const clickedElement = event.target as HTMLElement;
      const isCartElement = clickedElement.classList.contains('cart') ||
        clickedElement.classList.contains('items-num') ||
        clickedElement.classList.contains('fa-cart-plus');
      
      if (!isCartElement) {
        cartDetails.classList.remove('active');
        detailsArrow.classList.remove('active');
      }
    }
  }

  focusOut(event: FocusEvent, element: HTMLElement): void {
    let cartDetails = element.querySelector('.dropdown-div');
    cartDetails?.classList.remove('active');
    let detailsArrow = element.querySelector('.dropdown-arrow');
    detailsArrow?.classList.remove('active');
    console.log(event.target);
  }

  // Search
  searchResults: string[] = [];
  searchQuery: string = '';
  cartItems: IProduct[] = [];
  total: number = 0;
  totalOfOrder:number=0;
  constructor(private productApiPages: ProductsPagesService ,
     private router: Router,private cartService: CartService,private route: ActivatedRoute,
     private userService:UserService, private authService:AuthService){}

  ngOnInit(): void {
    this.localLang = localStorage.getItem('myLang') || "en";
    this.cartItems = this.cartService.getCartItemsWithQuantities();
    this.totalOfOrder = this.calculateTotalQuantities();
    this.route.queryParams.subscribe((params) => {
      this.total = parseFloat(params['total'] || 0);
    });
    this.isAuthorized = this.authService.isAuthenticated();
    this.currentUser = !!this.token ? jwtDecode(this.token) : null;
  }
calculateTotalQuantities(): number {
  return this.cartItems.reduce((total, item) => total + item.quantity, 0);
}

  setSearch(e: Event) {
    this.searchResults = [];
    let input = e.target as HTMLInputElement;

    this.productApiPages.searchFor(input.value)?.subscribe({
      next: data => {
        data.forEach(item => {
          item.name = this.localLang == 'ar' ? item.productNameAR : item.descriptionEN
          this.searchResults.push(item.name.slice(0,30) + " ...");
          this.searchResults = this.searchResults.slice(0, 10)
        })
      },
      error:err => console.log(err)
    })
  }

search(): void {
  if (this.searchQuery.trim() !== '') {
    this.searchResults = [];
    console.log(this.searchQuery);
    this.router.navigate(['home/searchResult'], {
        queryParams: { query: this.searchQuery },
      });
      // location.assign(`/home/searchResult?query=${this.searchQuery}`)
    }
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
  }

  toggleLanguage(){
    this.localLang == "en" ?
    localStorage.setItem("myLang", "ar") :
    localStorage.setItem("myLang", "en");

    // console.log(localStorage.getItem("myLang"));
    location.reload();
  }
  
  toggleMenu() {
    let menu = document.querySelector('ul.menu');
    menu?.classList.contains('active')
      ? menu?.classList.remove('active')
      : menu?.classList.add('active');
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
