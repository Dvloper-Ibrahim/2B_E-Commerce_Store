import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './Components/home-page/home-page.component';
import { NotFoundComponent } from './Components/not-found/not-found.component';
import { SignInComponent } from './Components/sign-in/sign-in.component';
import { CreateAccountComponent } from './Components/create-account/create-account.component';
import { CorporateSolutionsComponent } from './Components/corporate-solutions/corporate-solutions.component';
import { FreeShippingComponent } from './Components/home-page/free-shipping/free-shipping.component';
import { ProductDetailsComponent } from './Components/pages/product-details/product-details.component';
import { CartComponent } from './Components/cart/cart.component';
import { SearchResultComponent } from './Components/search-result/search-result.component';
import { CategoryDetailsComponent } from './Components/category-details/category-details.component';
import { SubCategoryDetailsComponent } from './Components/sub-category-details/sub-category-details.component';
import { WishListComponent } from './Components/wish-list/wish-list.component';
import { CheckOutComponent } from './Components/check-out/check-out.component';
import { AuthGuard } from './Guards/auth.guard';
import { SubSubCategoryDetailsComponent } from './Components/sub-sub-category-details/sub-sub-category-details.component';
import { PaymentMethodsComponent } from './Components/payment-methods/payment-methods.component';
import { PayPalComponent } from './Components/pay-pal/pay-pal.component';
import { SuccessPaymentComponent } from './Components/success-payment/success-payment.component';
import { UserProfileComponent } from './Components/user-profile/user-profile.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomePageComponent,
    title: '2B Egypt-Buy Online|laptops|Mobiles|Gaming best prices in Egypt',
  },
  {
    path: 'sign-in',
    component: SignInComponent,
    title: 'Sign In',
  },
  {
    path: 'user/account/create',
    component: CreateAccountComponent,
    title: 'Create Account',
  },
  {
    path: 'userProfile',
    component: UserProfileComponent,
    canActivate: [AuthGuard],
    title: 'My Profile',
  },
  {
    path: 'home/category-details/:categoryID',
    component: CategoryDetailsComponent,
    title: 'Category Details',
  },
  {
    path: 'home/subCategory-details/:subCategoryID',
    component: SubCategoryDetailsComponent,
    title: 'SubCategory Details',
  },
  {
    path: 'home/subSubCategory-details/:subSubCategoryID',
    component: SubSubCategoryDetailsComponent,
    title: 'SubSubCategory Details',
  },
  {
    path: 'product/:productId',
    component: ProductDetailsComponent,
    title: 'Product Details',
  },
  {
    path: 'home/corporateSolutions',
    component: CorporateSolutionsComponent,
    title: 'Corporate Solutions',
  },
  {
    path: 'home/shipping',
    component: FreeShippingComponent,
    title: 'Free Shipping',
  },
  {
    path: 'checkout',
    component: CheckOutComponent,
    canActivate: [AuthGuard],
    title: 'CHECKOUT',
  },
  {
    path: 'checkout/payment',
    component: PaymentMethodsComponent,
    canActivate: [AuthGuard],
    title: 'CHECKOUT',
  },
  {
    path: 'paymentMethod/paypal',
    component: PayPalComponent,
    canActivate: [AuthGuard],
    title: 'Paypal Checkout',
  },
  {
    path: 'successCheckout',
    component: SuccessPaymentComponent,
    canActivate: [AuthGuard],
    title: 'Success Checkout',
  },
  {
    path: 'home/wishList',
    component: WishListComponent,
    canActivate: [AuthGuard],
    title: 'My Wish List',
  },
  {
    path: 'home/searchResult',
    component: SearchResultComponent,
    title: 'Search Result',
  },
  { path: 'cart', component: CartComponent, title: 'My Cart' },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent, title: 'Not Found' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
