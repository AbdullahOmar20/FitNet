import { Component, inject, OnInit, Type } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "../layout/header/header.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  baseUrl = "https://localhost:5001/api/"
  private httpClient = inject(HttpClient);
  title = 'FitNet';

  products: Products[] = [];
  
  ngOnInit(): void {
    this.httpClient.get<Pagination<Products>>(this.baseUrl + "Products").subscribe({
      next: response => this.products = response.data,
      error: err => console.log("error" + err),
      complete: () => console.log("complete")
    })
  }
}

type Products = {
  id: number,
  name: string,
  description: string,
  price: number,
  pictureUrl: string,
  productType: string,
  productBrand: string
}

type Pagination<T> = {
  pageIndex: number,
  pageSize: number,
  count: number,
  data: T[]
}