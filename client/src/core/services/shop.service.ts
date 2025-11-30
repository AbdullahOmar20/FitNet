import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Products } from '../../shared/Models/Products';
import { Pagination } from '../../shared/Models/Pagination';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = "https://localhost:5001/api/"
  private httpClient = inject(HttpClient);

  types: string[] = [];
  brands: string[] = [];

  getProducts(){
    return this.httpClient.get<Pagination<Products>>(this.baseUrl + "Products?pageSize=20")
  }

  getBrands(){
    if(this.brands.length > 0) return this.brands;
    return this.httpClient.get<string[]>(this.baseUrl + "Products/brands").subscribe({
      next: Response => this.brands = Response
    })
  }
  getTypes(){
    if(this.types.length > 0) return this.types;
    return this.httpClient.get<string[]>(this.baseUrl + "Products/types").subscribe({
      next: Response => this.types = Response
    })
    
  }
}
