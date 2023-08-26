import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRating } from 'src/Model/i-rating';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class RatingApiServiceService {

  private http={};
  constructor(private httpClient:HttpClient) {
    this.http={
      headers:new HttpHeaders({'Content-Type':'application/json'})
    };
   }

   ratingProduct(newRate:IRating):Observable<IRating>{
    return this.httpClient.post<IRating>(`${environment.BaseApiUrl}/rating`,JSON.stringify(newRate),this.http);
   }

}
