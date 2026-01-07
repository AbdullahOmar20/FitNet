import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { Products } from '../../shared/Models/Products';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from "@angular/material/button";
import { MatInput, MatLabel } from "@angular/material/input";
import { MatIcon } from "@angular/material/icon";
import { MatFormField } from '@angular/material/form-field';
import { MatDivider } from "@angular/material/divider";
import { CartService } from '../../core/services/cart.service';

@Component({
    selector: 'app-product-details',
    imports: [
        CurrencyPipe,
        MatButton,
        MatIcon,
        MatFormField,
        MatInput,
        MatLabel,
        MatDivider
    ],
    templateUrl: './product-details.component.html',
    styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  private shopService = inject(ShopService);
  
  cartService = inject(CartService)

  private activatedRtoue = inject(ActivatedRoute);
  
  product?: Products

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(){
    const id = this.activatedRtoue.snapshot.paramMap.get('id');
    if(!id) return;

    this.shopService.getProduct(+id).subscribe({
      next: product => this.product = product,
      error: err => console.log(err)
    })
  }
}
