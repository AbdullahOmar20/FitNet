import { Component, inject } from '@angular/core';
import { CartService } from '../../core/services/cart.service';
import { CartItemsComponent } from "../cart-items/cart-items.component";
import { CartSummaryComponent } from "../../shared/components/cart-summary/cart-summary.component";

@Component({
    selector: 'app-cart',
    imports: [CartItemsComponent, CartSummaryComponent],
    templateUrl: './cart.component.html',
    styleUrl: './cart.component.scss'
})
export class CartComponent {
    cartService = inject(CartService);
}
