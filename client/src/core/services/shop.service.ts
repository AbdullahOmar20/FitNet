import { HttpClient } from '@angular/common/http';
import { inject, Injectable, Type } from '@angular/core';
import { Products } from '../../shared/Models/Products';
import { Pagination } from '../../shared/Models/Pagination';
import { Types } from '../../shared/Models/Types';
import { Brands } from '../../shared/Models/Brands';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = "https://localhost:5001/api/"
  private httpClient = inject(HttpClient);

  types: Types[] = [];
  brands: Brands[] = [];

  getProducts(){
    return this.httpClient.get<Pagination<Products>>(this.baseUrl + "Products?pageSize=20")
  }

  getBrands(){
    if(this.brands.length > 0) return this.brands;
    return this.httpClient.get<Brands[]>(this.baseUrl + "Products/brands").subscribe({
      next: Response => this.brands = Response
    })
  }
  getTypes(){
    if(this.types.length > 0) return this.types;
    return this.httpClient.get<Types[]>(this.baseUrl + "Products/types").subscribe({
      next: Response => this.types = Response
    })
    
  }
}
