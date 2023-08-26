import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomePageComponent } from './Components/home-page/home-page.component';
import { HeaderComponent } from './Components/header/header.component';
import { CartDropdownDirective } from './Directives/cart-dropdown.directive';
import { FooterComponent } from './Components/footer/footer.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NavBarComponent } from './Components/header/nav-bar/nav-bar.component';
import { CarouselHome1Component } from './Components/home-page/carousel-home1/carousel-home1.component';
import { HomeAppliancesOffersComponent } from './Components/home-page/home-appliances-offers/home-appliances-offers.component';
import { PersonalCareOffersComponent } from './Components/home-page/personal-care-offers/personal-care-offers.component';
import { RecommendedOfferComponent } from './Components/home-page/recommended-offer/recommended-offer.component';
import { Section2ButtonComponent } from './Components/home-page/section2-button/section2-button.component';
import { Section3IconComponent } from './Components/home-page/section3-icon/section3-icon.component';
import { HiddenNavBarComponent } from './Components/header/nav-bar/hidden-nav-bar/hidden-nav-bar.component';
import { NotFoundComponent } from './Components/not-found/not-found.component';
import { SignInComponent } from './Components/sign-in/sign-in.component';
import { CreateAccountComponent } from './Components/create-account/create-account.component';
import { CorporateSolutionsComponent } from './Components/corporate-solutions/corporate-solutions.component';
import { ProductHomeComponent } from './Components/home-page/product-home/product-home.component';
import {
  HTTP_INTERCEPTORS,
  HttpClient,
  HttpClientModule,
} from '@angular/common/http';
import { ProductsPagesComponent } from './Components/pages/products-pages/products-pages.component';
import { FreeShippingComponent } from './Components/home-page/free-shipping/free-shipping.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductDetailsComponent } from './Components/pages/product-details/product-details.component';
import { RatingProductComponent } from './Components/rating-product/rating-product.component';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SideCaroselComponent } from './Components/pages/product-details/side-carosel/side-carosel.component';
import { CartComponent } from './Components/cart/cart.component';
import { Location } from '@angular/common';
import { SearchResultComponent } from './Components/search-result/search-result.component';
import { CategoryDetailsComponent } from './Components/category-details/category-details.component';
import { SubCategoryDetailsComponent } from './Components/sub-category-details/sub-category-details.component';
import { LanguageInterceptor } from './Interceptors/language.interceptor';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { FilteredProductsComponent } from './Components/filtered-products/filtered-products.component';
import { WishListComponent } from './Components/wish-list/wish-list.component';
import { FormatDiscountPipe } from './Components/pipes/format-discount.pipe';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CheckOutComponent } from './Components/check-out/check-out.component';
import { PaymentMethodsComponent } from './Components/payment-methods/payment-methods.component';
import { SubSubCategoryDetailsComponent } from './Components/sub-sub-category-details/sub-sub-category-details.component';
import { MainCaroselComponent } from './Components/pages/product-details/main-carosel/main-carosel.component';
import { MatchPasswordDirective } from './Directives/match-password.directive';
import { ValidateUserNameDirective } from './Directives/validate-user-name.directive';
import { NgxPayPalModule } from 'ngx-paypal';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { PayPalComponent } from './Components/pay-pal/pay-pal.component';
import { SuccessPaymentComponent } from './Components/success-payment/success-payment.component';
import { CartIconComponent } from './Components/cart/cart-icon/cart-icon.component';
import { CartInHiddenNavbarComponent } from './Components/cart/cart-in-hidden-navbar/cart-in-hidden-navbar.component';
import { UserProfileComponent } from './Components/user-profile/user-profile.component';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    HeaderComponent,
    CartDropdownDirective,
    FooterComponent,
    NavBarComponent,
    CarouselHome1Component,
    HomeAppliancesOffersComponent,
    PersonalCareOffersComponent,
    RecommendedOfferComponent,
    Section2ButtonComponent,
    Section3IconComponent,
    HiddenNavBarComponent,
    NotFoundComponent,
    SignInComponent,
    CreateAccountComponent,
    CorporateSolutionsComponent,
    ProductHomeComponent,
    ProductsPagesComponent,
    FreeShippingComponent,
    FormatDiscountPipe,
    ProductDetailsComponent,
    RatingProductComponent,
    // ProductCaroselComponent,
    SideCaroselComponent,
    CartComponent,
    // MobileTabletComponent,
    // ProductsByCategoryComponent,
    // GamingComponent,
    // HomeAppliancesComponent,
    // AccessoriesComponent,
    // ProductsBySubCategoryComponent,
    SearchResultComponent,
    CategoryDetailsComponent,
    SubCategoryDetailsComponent,
    FilteredProductsComponent,
    WishListComponent,
    CheckOutComponent,
    PaymentMethodsComponent,
    SubSubCategoryDetailsComponent,
    MainCaroselComponent,
    MatchPasswordDirective,
    ValidateUserNameDirective,
    PayPalComponent,
    SuccessPaymentComponent,
    CartIconComponent,
    CartInHiddenNavbarComponent,
    UserProfileComponent,
  ],

  imports: [
    BrowserModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
    AppRoutingModule,
    BrowserAnimationsModule,
    NgbModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    NgxPayPalModule,
  ],
  providers: [
    Location,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LanguageInterceptor,
      multi: true,
    },
    HttpClient,
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}
