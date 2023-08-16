import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IOrder } from 'src/Model/i-order';
import { IShipping } from 'src/Model/i-shipping';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private postOptions = {};
  constructor(private httpClient: HttpClient) {
    this.postOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };
  }

  placeOrder(order: IOrder): Observable<IOrder> {
    return this.httpClient.post<IOrder>(
      `${environment.BaseApiUrl}/api/order/create`,
      JSON.stringify(order),
      this.postOptions
    );
  }

  setShipping(shipping: IShipping): Observable<IShipping> {
    return this.httpClient.post<IShipping>(
      `${environment.BaseApiUrl}/api/Order/AddShipping`,
      JSON.stringify(shipping),
      this.postOptions
    );
  }
}
