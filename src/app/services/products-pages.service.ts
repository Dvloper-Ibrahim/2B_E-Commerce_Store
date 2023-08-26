import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IProduct } from 'src/Model/i-product';
import { IProductsPages } from 'src/Model/i-products-pages';
import { InformatinTableProducts } from 'src/Model/informatin-table-products';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ProductsPagesService {
  emit(sortedProduct: any) {
    throw new Error('Method not implemented.');
  }

  constructor(private httpClient : HttpClient) {
    // this.loadBrandsAndCounts();
  }

  getAllProduct():Observable<IProduct[]>{
    return this.httpClient.get<IProduct[]>(`${environment.BaseApiUrl}/api/product`);
  }

  getproductsById(prodId:number):Observable<IProduct>{
    return this.httpClient.get<IProduct>(`${environment.BaseApiUrl}/api/product/${prodId}`)
  }

  getproductsByName(prodName:string):Observable<IProductsPages>{
    return this.httpClient.get<IProductsPages>(`${environment.BaseApiUrl}/productsPages/${prodName}`)
  }



  getProductInformationTable(): Observable<InformatinTableProducts[]> {
    return this.httpClient.get<InformatinTableProducts[]>(`${environment.BaseApiUrl}/productsPages`);
  }


  getProductsByCategoryId(categoryId: number): Observable<IProduct[]> {
    return this.httpClient.get<IProduct[]>(`${environment.BaseApiUrl}/api/product/category/${categoryId}`);
  }

  getProductsBySubCategoryId(subCategoryId: number): Observable<IProduct[]> {
    return this.httpClient.get<IProduct[]>(`${environment.BaseApiUrl}/api/Product/parentSubCategory/${subCategoryId}`);
  }

  getProductsBySubSubCategoryId( subSubCategoryId: number): Observable<IProduct[]> {
    return this.httpClient.get<IProduct[]>(`${environment.BaseApiUrl}/api/Product/childSubCategory/${subSubCategoryId}`);
  }

  
  searchFor(str: string): Observable<IProduct[]> | undefined{
    if(!!str){
      return this.httpClient.get<IProduct[]>(`${environment.BaseApiUrl}/api/Product/search?query=${str}`)
    }
    return undefined;
  }

  ///     Filter for brand

  private brands: string[] = [];
  private brandCount: { [brand: string]: number } = {};

  //  Search Product

  searchProduct(prodName:string):Observable<IProductsPages[]>{
    return this.httpClient.get<IProductsPages[]>(`${environment.BaseApiUrl}/productsPages/${prodName}`);
  }

  getBrands(): string[] {
    return this.brands;
  }

  getBrandCount(brand: string): number {
    return this.brandCount[brand] || 0;
  }


}


