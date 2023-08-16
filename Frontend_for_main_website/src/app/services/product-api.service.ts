import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IProduct } from 'src/Model/i-product';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ProductApiService {

  constructor(private httpClient : HttpClient) { }

  getAllProduct():Observable<IProduct[]>{
    return this.httpClient.get<IProduct[]>(`${environment.BaseApiUrl}/products`);
  }

  getproductsById(prodID:number):Observable<IProduct>{
    return this.httpClient.get<IProduct>(`${environment.BaseApiUrl}/products/${prodID}`)
  }
}
