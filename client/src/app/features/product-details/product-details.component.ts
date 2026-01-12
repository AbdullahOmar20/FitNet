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
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-product-details',
    imports: [
        CurrencyPipe,
        MatButton,
        MatIcon,
        MatFormField,
        MatInput,
        MatLabel,
        MatDivider,
        FormsModule
    ],
    templateUrl: './product-details.component.html',
    styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  private shopService = inject(ShopService);
  
  cartService = inject(CartService)

  private activatedRtoue = inject(ActivatedRoute);
  
  product?: Products
  Quantity = 1;
  QuantityInCart = 0;

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(){
    const id = this.activatedRtoue.snapshot.paramMap.get('id');
    if(!id) return;

    this.shopService.getProduct(+id).subscribe({
      next: product => {
        this.product = product;
        this.updateQuantityInCart();
      },
      error: err => console.log(err)
    })
  }

  updateQuantityInCart(){
    if(!this.product) return;
    this.QuantityInCart = this.cartService.cart()?.items.find(x => x.id === this.product?.id)?.quantity || 0;
    
    this.Quantity = this.QuantityInCart || 1;

    // this.cartService.addItemToCart(this.product, this.Quantity);
    // this.QuantityInCart = this.Quantity;
  }

  getButtonText(){
    return this.QuantityInCart > 0 ? 'Update cart' : 'Add to cart'
  }

  updateCart(){
    if(!this.product) return;
    if(this.Quantity > this.QuantityInCart){
      const itemsToBeAdd = this.Quantity - this.QuantityInCart
      this.QuantityInCart += itemsToBeAdd;
      this.cartService.addItemToCart(this.product, itemsToBeAdd);
    }
    else{
      const itemsToBeRemoved = this.QuantityInCart - this.Quantity;
      this.QuantityInCart -= itemsToBeRemoved;
      this.cartService.removeItemFromCart(this.product.id, itemsToBeRemoved);
    }
    
  }
}
