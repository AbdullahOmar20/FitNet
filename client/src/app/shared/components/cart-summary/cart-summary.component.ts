import { Component, inject } from '@angular/core';
import { RouterLink } from "@angular/router";
import { MatAnchor } from "@angular/material/button";
import { MatFormField, MatInput, MatLabel } from '@angular/material/input';
import { CartService } from '../../../core/services/cart.service';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-cart-summary',
  imports: [RouterLink, MatAnchor, MatFormField, MatLabel, MatInput, CurrencyPipe],
  templateUrl: './cart-summary.component.html',
  styleUrl: './cart-summary.component.scss',
})
export class CartSummaryComponent {
  cartService = inject(CartService);
  
}
