import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IProduct } from 'src/Model/i-product';
import { IProductsPages } from 'src/Model/i-products-pages';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ProductFilterService {

  constructor(private httpClient : HttpClient ) { }

  filterByCategoryId(categoryId: number): Observable<IProduct[]> {
    return this.httpClient.get<IProduct[]>(`${environment.BaseApiUrl}/api/Product/category/${categoryId}`)
  }
  
  filterBySubCategoryId(subCategoryId: number): Observable<IProduct[]> {
    return this.httpClient.get<IProduct[]>(`${environment.BaseApiUrl}/api/Product/parentSubCategory/${subCategoryId}`)
  }
  
  filterBySubSubCategoryId(subSubCategoryId: number): Observable<IProduct[]> {
    return this.httpClient.get<IProduct[]>(`${environment.BaseApiUrl}/api/Product/childSubCategory/${subSubCategoryId}`)
  }


  // filterProductsByCategories(products: IProductsPages[], categoryId: number, subCategoryId: number, subSubCategoryId: number): IProductsPages[] {
  //   return products.filter((product) => {
  //     if (categoryId && product.categoryId !== categoryId) {
  //       return false;
  //     }
  //     if (subCategoryId && product.subCategoryId !== subCategoryId) {
  //       return false;
  //     }
  //     if (subSubCategoryId && product.subSubCategoryId !== subSubCategoryId) {
  //       return false;
  //     }
  //     return true;
  //   });
  // }


  // getProductsByCategoryId(categoryId: number): Observable<IProductsPages[]> {
  //   return this.httpClient.get<IProductsPages[]>(`${environment.BaseApiUrl}/productsPages/${categoryId}`);
  // }

  // getProductsBySubSubCategoryId(subSubCategoryId: number): Observable<IProductsPages[]> {
  //   return this.httpClient.get<IProductsPages[]>(`${environment.BaseApiUrl}/productsPages/categoryId/subCategoryId/${subSubCategoryId}`);
  // }

  }
  
