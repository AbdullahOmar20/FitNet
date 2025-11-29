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

  getProducts(){
    return this.httpClient.get<Pagination<Products>>(this.baseUrl + "Products")
  }
}
