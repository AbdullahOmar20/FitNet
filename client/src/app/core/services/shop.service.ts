import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, Type } from '@angular/core';
import { Products } from '../../shared/Models/Products';
import { Pagination } from '../../shared/Models/Pagination';
import { Types } from '../../shared/Models/Types';
import { Brands } from '../../shared/Models/Brands';
import { ShopParams } from '../../shared/Models/ShopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = "https://localhost:5001/api/"
  private httpClient = inject(HttpClient);

  types: Types[] = [];
  brands: Brands[] = [];

  getProducts(shopParams: ShopParams){
    let params = new HttpParams();
    if(shopParams.brands && shopParams.brands.length > 0)
    {
      shopParams.brands.forEach(brand => {
        params = params.append('brands', brand)
      });
    }
    
    if(shopParams.types && shopParams.types.length > 0)
    {
      shopParams.types.forEach(type => {
        params = params.append('types', type)
      });
    }

    if(shopParams.sort){
      params = params.append('sort', shopParams.sort)
    }
    if(shopParams.search){
      params = params.append('search', shopParams.search)
    }

    params = params.append('pageSize', shopParams.pageSize)
    params = params.append('pageIndex', shopParams.pageNumber)
    return this.httpClient.get<Pagination<Products>>(this.baseUrl + "Products", {params})
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

  getProduct(id: number){
    return this.httpClient.get<Products>(this.baseUrl + "Products/" + id);
  }
}
