import { Component, inject, OnInit, Type } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "../layout/header/header.component";
import { Products } from '../shared/Models/Products';
import { ShopService } from './core/services/shop.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  shopService = inject(ShopService)
  title = 'FitNet';

  products: Products[] = [];
  
  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      next: response => this.products = response.data,
      error: err => console.log("error" + err)
    })
  }
}
