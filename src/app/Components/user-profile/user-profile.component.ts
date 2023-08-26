import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { IProduct } from 'src/Model/i-product';
import { AuthService, DecodedJWT } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { ProductsPagesService } from 'src/app/services/products-pages.service';
import { SideMenuNavbarService } from 'src/app/services/side-menu-navbar.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit{
  isAuthorized: boolean = false;
  currentUser = {} as DecodedJWT | null;
  userID: string = "";
  token: string | null = localStorage.getItem('_2B_User');
  totalOfOrder:number=0;
  cartItems: IProduct[] = [];

  constructor(private router: Router ,
    private productApiPages: ProductsPagesService,
    private sideMenuNavbarService:SideMenuNavbarService,
    private cartService: CartService,
    private userService:UserService, private authService:AuthService) { }

  ngOnInit(): void {
    this.isAuthorized = this.authService.isAuthenticated();
    this.currentUser = !!this.token ? jwtDecode(this.token) : null;
    this.userID = this.currentUser?.nameid || "";
    this.cartItems = this.cartService.getCartItemsWithQuantities();
    this.totalOfOrder = this.calculateTotalQuantities();
  }

   //function cartItems
   calculateTotalQuantities(): number {
    return this.cartItems.reduce((total, item) => total + item.quantity, 0);
  }
  

  // في ملف الكومبوننت
deleteButtonDisabled: boolean = false;
showDeleteConfirmation: boolean = false;

deleteAccount() {
  // console.log("Account deleted!");
  
  // this.showDeleteConfirmation = false;
  // this.deleteButtonDisabled = false;
  this.userService.deleteAccount().subscribe({
    next: data => {
      alert(data.message)
      location.assign("/home");
    },
    error: err => console.log(err)
  })
}

cancelDelete() {
  this.showDeleteConfirmation = false;
  this.deleteButtonDisabled = false;
}


}
