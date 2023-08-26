import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICategory } from 'src/Model/i-category';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private httpClient: HttpClient) { }

  getAllCategories():Observable<ICategory[]>{
    return this.httpClient.get<ICategory[]>(`${environment.BaseApiUrl}/api/category`);
  }
  
  getCategoryById(catId:number):Observable<ICategory>{
    return this.httpClient.get<ICategory>(`${environment.BaseApiUrl}/api/category/${catId}`)
  }
}
