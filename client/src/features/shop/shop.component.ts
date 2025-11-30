import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Products } from '../../shared/Models/Products';
import { ProductItemComponent } from "../product-item/product-item.component";

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    ProductItemComponent
],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit{
  shopService = inject(ShopService)

  products: Products[] = [];
  
  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop(){
    this.shopService.getBrands()
    this.shopService.getTypes()
    this.shopService.getProducts().subscribe({
      next: response => this.products = response.data,
      error: err => console.log("error" + err)
    })
  }
}
