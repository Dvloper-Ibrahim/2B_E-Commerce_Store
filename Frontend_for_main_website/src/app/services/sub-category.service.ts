import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ISubCategory } from 'src/Model/i-sub-category';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class SubCategoryService {

  constructor(private httpClient: HttpClient) { }

  getSubCategoryByID(subCatId : number):Observable<ISubCategory>{
    return this.httpClient.get<ISubCategory>(`${environment.BaseApiUrl}/api/SubCategory/${subCatId}`);
  }

  getSubCategoriesInCategory(catId : number):Observable<ISubCategory[]>{
    return this.httpClient.get<ISubCategory[]>(`${environment.BaseApiUrl}/api/SubCategory/parent_in_category/${catId}`);
  }
  
  getSubCategoriesInSubCategory(subCatId:number):Observable<ISubCategory[]>{
    return this.httpClient.get<ISubCategory[]>(`${environment.BaseApiUrl}/api/SubCategory/child_to_subCategory/${subCatId}`)
  }
}
