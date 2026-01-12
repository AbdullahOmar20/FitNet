import { Component, inject, input } from '@angular/core';
import { RouterLink } from "@angular/router";
import { CartItems } from '../../shared/Models/cart';
import { MatIconButton } from "@angular/material/button";
import { MatIcon } from "@angular/material/icon";
import { CurrencyPipe } from '@angular/common';
import { CartService } from '../../core/services/cart.service';

@Component({
  selector: 'app-cart-items',
  imports: [RouterLink, MatIconButton, MatIcon, CurrencyPipe],
  templateUrl: './cart-items.component.html',
  styleUrl: './cart-items.component.scss',
})
export class CartItemsComponent {
  item = input.required<CartItems>();

  cartService = inject(CartService);

  increment(){
    this.cartService.addItemToCart(this.item())
  }
  decrement(){
    this.cartService.removeItemFromCart(this.item().id)
  }
  removeFromCart(){
    this.cartService.removeItemFromCart(this.item().id, this.item().quantity)
  }
}
